namespace AppCourseUtils;

public static class StringUtils
{
    public static string Reverse(string s)
    {
        string result = string.Empty;
        for (int i = s.Length - 1; i >= 0; --i)
        {
            result += s[i];
        }
        return result;
    }

    public static string Reverse2(string s)
    {
        char[] chars = new char[s.Length];

        for (int i = 0; i < s.Length; i++)
        {
            chars[i] = s[s.Length - i - 1];
        }
        return new string(chars);
    }

    public static bool IsPalindrome1(string s) =>
        s == Reverse2(s);

    public static bool IsPalindrome2(string s)
    {   // len = 7
        //   0 1 2 3 4 5 6
        //   o t t e t t o

        // 0 1 2 3 
        // a b b a

        for (int i = 0; i < s.Length / 2; i++)
        {
            if (s[i] != s[s.Length - 1 - i])
            {
                return false;
            }
        }
        return true;
    }
}
