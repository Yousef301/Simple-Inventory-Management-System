namespace SimpleInventoryManagementSystem;

public class Validator
{
    public static string TruncateString(string value, int maxLength) =>
        value.Length > maxLength ? value[..maxLength] : value;

    public static T IsValid<T>(T newValue, T oldValue, T min) where T : IComparable<T> =>
        newValue.CompareTo(min) > 0 ? newValue : oldValue;

    public static T CheckIfNumeric<T>(string value, string attribute)
    {
        do
        {
            if (typeof(T) == typeof(int))
            {
                if (int.TryParse(value, out var result))
                {
                    return (T)(object)result;
                }
            }
            else if (typeof(T) == typeof(double))
            {
                if (double.TryParse(value, out var result))
                {
                    return (T)(object)result;
                }
            }

            Log.InvalidInputMessage("Invalid input. Please try again.");
            value = Menu.ReadFromUser(attribute);
        } while (true);
    }
}