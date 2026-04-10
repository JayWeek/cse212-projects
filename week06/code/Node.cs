public class Node
{
    public int Data { get; set; }
    public Node? Right { get; private set; }
    public Node? Left { get; private set; }

    public Node(int data)
    {
        this.Data = data;
    }

    public void Insert(int value)
    {
        // TODO Start Problem 1
        if (value == Data)
        {
            return; //We do nothing since its a duplicate
        }

        if (value < Data)
        {
            // Insert to the left
            if (Left is null)
                Left = new Node(value);
            else
                Left.Insert(value);
        }
        else
        {
            // Insert to the right
            if (Right is null)
                Right = new Node(value);
            else
                Right.Insert(value);
        }
    }

    public bool Contains(int value)
    {
        // TODO Start Problem 2
        if (value == Data) return true;
        
        if (value < Data)
            return Left != null && Left.Contains(value);
        
        return Right != null && Right.Contains(value);
        
    }

    public int GetHeight()
    {
        int leftHeight = 0;
        int rightHeight = 0;

        // 1. Calculate the height of the left side
        if (Left != null)
        {
            leftHeight = Left.GetHeight();
        }
        // (If Left is null, leftHeight stays 0)

        // 2. Calculate the height of the right side
        if (Right != null)
        {
            rightHeight = Right.GetHeight();
        }
        // (If Right is null, rightHeight stays 0)

        // 3. Find which side is bigger
        int maxChildHeight;
        if (leftHeight > rightHeight)
        {
            maxChildHeight = leftHeight;
        }
        else
        {
            maxChildHeight = rightHeight;
        }

        // 4. Return the maximum side plus 1 (for this current node)
        return maxChildHeight + 1;
    }

}