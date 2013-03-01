namespace Bendras.SocialMarket.Web.Services

open System
open ServiceStack.Common
open ServiceStack.ServiceInterface
open ServiceStack.ServiceHost
open ServiceStack.WebHost.Endpoints
open ServiceStack.ServiceInterface.ServiceModel

// ------------------ Models -------------------

type HelloResponse() = 
    member val Message = "" with get, set
    member val ResponseStatus : ResponseStatus = null with get, set

type Hello() = 
    interface IReturn<HelloResponse>
    member val Name = "" with get, set

// ------------------ Service -------------------

type HelloService() =
    inherit Service()
    member this.Any(model:Hello) = 
        let response = new HelloResponse()
        response.Message <- "Hello, " + model.Name
        
        response


