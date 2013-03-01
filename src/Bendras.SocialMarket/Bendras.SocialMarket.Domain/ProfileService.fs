namespace Bendras.SocialMarket.Domain

open System
open System.Linq
open Bendras.SocialMarket.Model
open Microsoft.FSharp.Data.TypeProviders

type ProfileService() = 

    let db = DbSchema.GetDataContext()

    member this.getProfile (id : Guid) : Profile =
        let dbProfile = query { 
            for p in db.Profile do 
            where (p.Id = id)
            head 
         }
        let profile = new Profile(Id = dbProfile.Id, Name = dbProfile.Name)
        profile

    member this.updateProfile (profile : Profile) : Profile =
        let dbProfile = query { 
            for p in db.Profile do 
            where (p.Id = profile.Id)
            head 
        }
        dbProfile.Name <- profile.Name
        this.getProfile(profile.Id)
