using UnityEngine;

public class CheatCode : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private SizeSlot _sizeSlot;
    [SerializeField] private Collector _collector;
    [SerializeField] private Stack _stack;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _wallet.Cheat();
            _collector.Cheat();
        }
    }
}