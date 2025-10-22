using System;
using System.Runtime.CompilerServices;
using System.Text;
using TMPro;
using UnityEngine;

public class TimeView : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private readonly StringBuilder sb = new StringBuilder(16);
    private DateTime now;

    void Update()
    {
        if (!gameObject.activeSelf)
            return;

        now = DateTime.Now;

        sb.Clear();

        AppendDigits(sb, now.Hour, 2);
        sb.Append(':');
        AppendDigits(sb, now.Minute, 2);
        sb.Append(':');
        AppendDigits(sb, now.Second, 2);
        sb.Append('.');
        AppendDigits(sb, now.Millisecond, 3);

        text.SetText(sb);
    }

    private void AppendDigits(StringBuilder sb, int value, int minDigits)
    {
        Span<char> buffer = stackalloc char[3];
        int index = buffer.Length;
        int digits = 0;

        do
        {
            int digit = value % 10;
            value /= 10;
            buffer[--index] = (char)('0' + digit);
            digits++;
        } while (value > 0 || digits < minDigits);

        sb.Append(buffer[index..]);
    }
}
