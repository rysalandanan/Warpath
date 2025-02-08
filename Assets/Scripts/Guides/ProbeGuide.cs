using UnityEngine;

public class ProbeGuide : MonoBehaviour
{
    [SerializeField] private GameObject wavePrompt;
    [SerializeField] private GameObject minimizedWavePrompt;

    [SerializeField] private GameObject guideProbe;

    [SerializeField] private Transform startingPos;


    private void Update()
    {
        if (wavePrompt.activeInHierarchy || minimizedWavePrompt.activeInHierarchy)
        {
            SpawnGuideProbe();
        }
    }
    private void SpawnGuideProbe()
    {
        if (guideProbe == null) return;
        if (GameObject.FindWithTag("GuideProbe") == null)
        {
            Instantiate(guideProbe, startingPos.position, Quaternion.identity);
        }
    }
}
