namespace GuestSharedModel.Enums
{
    public enum Title
    {
        Mr,
        Mrs,
        Ms,
        Dr,
        Prof
    }

    public enum Country
    {
        US,
        UK,
        FR,
        DE,
        CN,
        IN,
        BR,
        AU,
        NL,
        SE
    }

    public enum GuestStatus
    {
        PhoneRequired,
        NameRequired,
        PhoneInvalid,
        Exists,
        Created, 
        ErrorInCreation,
        Modified,
        ErrorWhileUpdate,
        DuplicatePhone,
        PhoneAdded,
        PhoneAddError
    }

    public enum UserStatus
    {
        DuplicateName,
        DuplicateEmail,
        Added,
        ErrorInCreation,
        New
    }
}
