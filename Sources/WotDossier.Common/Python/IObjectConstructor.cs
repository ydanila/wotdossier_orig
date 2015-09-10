namespace WotDossier.Common.Python
{
    /// <summary>
    /// Interface for object Constructors that are used by the unpickler
    /// to create instances of non-primitive or custom classes.
    /// </summary>
    public interface IObjectConstructor
    {
        /**
        * Create an object. Use the given args as parameters for the constructor.
        */
        object construct(object[] args);
    }
}