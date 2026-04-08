using System.Collections;

public interface IXPService
{
    IEnumerator AddXP(int amount, System.Action<XPResponse> onSuccess);
}