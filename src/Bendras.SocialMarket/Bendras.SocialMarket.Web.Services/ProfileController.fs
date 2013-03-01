namespace Bendras.SocialMarket.Web.Services

open System
open System.Linq
open ServiceStack.Common
open ServiceStack.ServiceInterface
open ServiceStack.ServiceHost
open ServiceStack.ServiceInterface.ServiceModel
open ServiceStack.WebHost.Endpoints
open Bendras.SocialMarket.Domain
open Bendras.SocialMarket.Model

// ------------------ Response Models -------------------

type ProfileResponse() = 
    member val ResponseStatus : ResponseStatus = null with get, set
    member val Profile = new Profile() with get, set 
    member val Profiles = Seq.empty<Profile> with get, set 

// ------------------ Request Models -------------------

[<Route("/Profile/{Id}")>]
type GetProfile() = 
    interface IReturn<ProfileResponse>
    member val Id = "" with get, set

[<Route("/Profile/Update/{Id}")>]
type UpdateProfile() = 
    interface IReturn<ProfileResponse>
    member val Id = "" with get, set
    member val Name = "" with get, set

// ------------------ Service -------------------

type ProfileController(profileService: ProfileService) =
    inherit Service()

    // create an active pattern
    let (|Int|_|) str =
       match System.Int32.TryParse(str) with
       | (true,int) -> Some(int)
       | _ -> None

    // create an active pattern
    let (|Bool|_|) str =
       match System.Boolean.TryParse(str) with
       | (true,bool) -> Some(bool)
       | _ -> None

    // create an active pattern
    let (|Guid|_|) str =
       match System.Guid.TryParse(str) with
       | (true,guid) -> Some(guid)
       | _ -> None
    
    member this.Get(model:GetProfile) : ProfileResponse = 
        match model.Id with
            | Guid id -> new ProfileResponse(Profile = profileService.getProfile(id))
            | _ -> new ProfileResponse()

    member this.Post(model:UpdateProfile) : ProfileResponse =
        match model.Id with
            | Guid id -> 
                let profile = profileService.getProfile(id)
                profile.Name <- model.Name
                new ProfileResponse(Profile = profileService.updateProfile(profile))
            | _ -> new ProfileResponse()
        
