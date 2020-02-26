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

    use stream = File.OpenRead @"C:\Users\Schorsch\AppData\Local\fontsquirrel\mplus-1m-regular.ttf"
    let reader = OpenFontReader()
    let typeface = reader.Read(stream, 0, ReadFlags.Full)

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
