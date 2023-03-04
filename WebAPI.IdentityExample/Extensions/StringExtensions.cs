public static class StringExtesions
{
    public static string Capitalize(this string str)
    {
        if (str.Length < 2)
            return str.ToUpper();

        return string.Join("", str.Select((ch, id) =>
        {
            if (id == 0)
                return char.ToUpper(ch);

            return char.ToLower(ch);
        }).ToArray());
    }
}