open System
open ServiceStack.Common
open ServiceStack.ServiceInterface
open ServiceStack.ServiceHost
open ServiceStack.WebHost.Endpoints

// http://research.microsoft.com/en-us/um/cambridge/projects/fsharp/manual/spec.html
// http://en.wikibooks.org/wiki/F_Sharp_Programming/Interfaces
 
[<Route("/t", Summary = "Test1111", Notes = "Some notes")>]
[<Route("/t/{Name}")>]
type Hello() = 
    interface IReturn<string>
    member val Name = "" with get, set

type HelloService() =
    inherit Service()
    member this.Get (model:Hello) = 
        "Hello, " + model.Name
 
//Define the Web Services AppHost
type AppHost =
    inherit AppHostHttpListenerBase
    new() = { inherit AppHostHttpListenerBase("Hello F# Services", typeof<HelloService>.Assembly) }
    override this.Configure container = 
        ignore 0

//Run it!
[<EntryPoint>]
let main args =
    let host = if args.Length = 0 then "http://*:9999/" else args.[0]
    printfn "listening on %s ..." host
    let appHost = new AppHost()
    appHost.Init()
    appHost.Start host

    let isActive = ref true
    let finished b = 
        isActive := b
    
    let pi = System.Diagnostics.Process.Start(@"C:\Program Files\Internet Explorer\iexplore.exe", "http://127.0.0.1:9999");
    pi.EnableRaisingEvents <- true
    pi.Exited.Add (fun ea -> finished false )
    
    while !isActive do
        System.Threading.Thread.Sleep(50)

    //Console.ReadLine() |> ignore
    0