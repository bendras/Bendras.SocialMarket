module Bendras.SocialMarket.Web.App.MainApp
//module MainApp

open System
open ServiceStack.Common
open ServiceStack.ServiceInterface
open ServiceStack.ServiceHost
open ServiceStack.WebHost.Endpoints

// http://msdn.microsoft.com/en-us/library/dd233181.aspx                                    - MSDN documentation.
// http://en.wikibooks.org/wiki/F_Sharp_Programming/Interfaces                              - WIKI book
// http://www.ienablemuch.com/2012/12/self-hosting-servicestack-serving.html                - Razor autocomplete


    ///Main entry point for the app.
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
        
        let pi = System.Diagnostics.Process.Start(@"C:\Program Files\Internet Explorer\iexplore.exe", "http://127.0.0.1:9999/html/reply/HelloService/Hello");
        pi.EnableRaisingEvents <- true
        pi.Exited.Add (fun ea -> finished false )
        
        while !isActive do
            System.Threading.Thread.Sleep(50)
    
        //Console.ReadLine() |> ignore
        0
