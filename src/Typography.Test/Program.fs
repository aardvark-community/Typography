// Learn more about F# at http://fsharp.org

open System
open Typography.OpenFont
open System.IO


let printComponents (typeface : Typeface) (c : char) =
    let idx = typeface.GetGlyphIndex(int c)
    let glyph = typeface.Glyphs.[int idx]


    


    let components =
        glyph.EndPoints |> Array.mapi (fun i stop ->
            let stop = int stop
            let start = if i > 0 then int glyph.EndPoints.[i-1] else 0
            Array.sub glyph.GlyphPoints start (1 + stop - start)
        )

    for i in 0 .. components.Length - 1 do
        printfn "%d" i

        for pt in components.[i] do
            
            printfn "  %A %A" pt.X pt.Y
         

[<EntryPoint>]
let main argv =

    use stream = File.OpenRead @"C:\windows\fonts\seguiemj.ttf"
    let reader = OpenFontReader()
    let typeface = reader.Read(stream, 0, ReadFlags.Full)

    let str = "😁😂"
    let arr = str.ToCharArray()

    let pre = System.Text.Encoding.Unicode.GetPreamble()
    printfn "%A" pre
    let mutable i = 0
    while i < arr.Length do
        let mutable cc = 1

        if (int arr.[i] &&& 0xF800) = 0xD800 then
            cc <- 2

        if cc = 1 then 
            let code = int arr.[i]
            printfn "code: %A" (typeface.GetGlyphIndex(code))
        elif cc = 2 then    
            let v0 = uint16 arr.[i] &&& 0x27FFus
            let v1 = uint16 arr.[i+1] &&& 0x23FFus
            let code = 0x10000u ||| (uint32 v0 <<< 10) ||| uint32 v1
            printfn "code: %A" (typeface.GetGlyphIndex(int code))

        i <- i + cc

    let otherCode = if arr.Length > 1 then (int arr.[0] <<< 16) ||| int str.[1] else int arr.[0]
        
    //let g = typeface.GetGlyphIndex(code)
    //let gg = typeface.GetGlyphByIndex(g)

    let d = typeface.GetKernDistance(typeface.GetGlyphIndex(int 'W'), typeface.GetGlyphIndex(int 'W'))
    printfn "%A" d
    //printComponents typeface 'a'

    

    

    //let mutable start = 0
    //for i in 0 .. glyph.EndPoints.Length - 1 do
    //    let stop = glyph.EndPoints.[i] |> int
    //    let comp = Array.sub glyph.GlyphPoints start (1 + stop - start)
    //    printfn "%A" comp
    //    start <- stop + 1
        
    
    0 // return an integer exit code
