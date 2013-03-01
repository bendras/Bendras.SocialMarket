namespace Bendras.SocialMarket.Model

type Profile() = 
    member val Id = System.Guid.Empty with get, set
    member val Name = "" with get, set
    member val Status = "" with get, set
