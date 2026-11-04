public static class Arrays
{
    /// <summary>
    /// This function produces an array of size 'length' starting with
    /// 'number' followed by multiples of 'number'.
    /// </summary>
    /// <returns>Array of doubles containing the multiples.</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // PLAN:
        // 1. Create a new double array with the requested length.
        // 2. Use a for loop to move through every position in the array.
        // 3. For each position, calculate the multiple of the supplied number.
        // 4. Store the calculated multiple at the corresponding array index.
        // 5. Return the completed array.

        double[] multiples = new double[length];

        for (int i = 0; i < length; i++)
        {
            multiples[i] = number * (i + 1);
        }

        return multiples;
    }

    /// <summary>
    /// Rotate the data to the right by the specified amount.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // PLAN:
        // 1. Check whether the list is empty. If it is, return without changes.
        // 2. Calculate the effective rotation amount using the list length.
        // 3. If the amount is negative, adjust it to an equivalent positive rotation.
        // 4. Repeat the rotation for the calculated amount.
        // 5. Save the last element, remove it from the list,
        //    and insert it at the beginning.

        if (data.Count == 0)
        {
            return;
        }

        amount = amount % data.Count;

        if (amount < 0)
        {
            amount += data.Count;
        }

        for (int i = 0; i < amount; i++)
        {
            int lastNumber = data[data.Count - 1];

            data.RemoveAt(data.Count - 1);

            data.Insert(0, lastNumber);
        }
    }
}