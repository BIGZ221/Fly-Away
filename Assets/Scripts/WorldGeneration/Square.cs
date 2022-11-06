public class Square {
    public ControlNode topLeft, topRight, bottomRight, bottomLeft;
    public Node topCenter, rightCenter, bottomCenter, leftCenter;

    public Square(ControlNode topLeft, ControlNode topRight, ControlNode bottomRight, ControlNode bottomLeft) {
        this.topLeft = topLeft;
        this.topRight = topRight;
        this.bottomRight = bottomRight;
        this.bottomLeft = bottomLeft;
        topCenter = topLeft.right;
        rightCenter = bottomRight.above;
        bottomCenter = bottomLeft.right;
        leftCenter = bottomLeft.above;
    }
}
