using UnityEngine;
using UnityEngine.Events;

public class VoidEventListener : MonoBehaviour
{
	[SerializeField] private VoidEventChannelSO m_channel = default;

	public UnityEvent OnEventRaised;

    private void OnEnable()
    {
		if (m_channel != null)
			m_channel.OnEventRaised += OnChannelEventRaised;
    }

    private void OnDisable()
    {
        if (m_channel != null)
            m_channel.OnEventRaised += OnChannelEventRaised;
    }

    private void OnChannelEventRaised()
    {
		if (OnEventRaised != null)
			OnEventRaised.Invoke();
    }
}

