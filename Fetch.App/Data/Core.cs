public class Core
{
    private static Core _instance = new Core();
    public static Core Instance
    {
        get { return _instance; }
    }
    public readonly string GOOGLE_MAP_API_KEY = "AIzaSyCjIaYOVYimuH3gY_857ojsss2TwMU6pNU";
}
