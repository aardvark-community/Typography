// Learn more about F# at http://fsharp.org

open System
open Typography.OpenFont
open System.IO


let printComponents (typeface : Typeface) (c : char) =
    let idx = typeface.LookupIndex(int c)
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
            
            printfn "  %A %A %A" pt.X pt.Y pt.OnCurve
         

[<EntryPoint>]
let main argv =

    use stream = File.OpenRead @"C:\windows\fonts\arial.ttf"
    let reader = OpenFontReader()
    let typeface = reader.Read(stream, ReadFlags.Full)

    let d = typeface.GetKernDistance(typeface.LookupIndex(int 'A'), typeface.LookupIndex(int 'W'))
    printfn "%A" d
    //printComponents typeface 'a'

    

    

    //let mutable start = 0
    //for i in 0 .. glyph.EndPoints.Length - 1 do
    //    let stop = glyph.EndPoints.[i] |> int
    //    let comp = Array.sub glyph.GlyphPoints start (1 + stop - start)
    //    printfn "%A" comp
    //    start <- stop + 1
        
    
    0 // return an integer exit code
