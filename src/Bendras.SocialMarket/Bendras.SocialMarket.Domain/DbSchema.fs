namespace Bendras.SocialMarket.Domain

open System
open System.Linq
open Bendras.SocialMarket.Model
open Microsoft.FSharp.Data.TypeProviders

type DbSchema = 
    SqlDataConnection<"Data Source=(local);Initial Catalog=BendrasSocialMarket;Integrated Security=SSPI;">