using UnityEngine;

public class ControlNode : Node {
    public Node above, right;

    public ControlNode(Vector3 position, float size) : base(position) {
        above = new Node(position + Vector3.forward * size / 2f);
        right = new Node(position + Vector3.right * size / 2f);
    }
}
