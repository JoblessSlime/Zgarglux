using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Split : MonoBehaviour
{
    public GameObject splitPrefab; // SplitPrefab is kinda the same but with the SplitAI script (in fact, maybe not)
    public SlimeInfos slimeInfos;

    public void SplitSlime(GameObject actualSlime)
    {
        GameObject split1 = Instantiate(splitPrefab, actualSlime.transform.position, Quaternion.identity);
        GameObject split2 = Instantiate(splitPrefab, actualSlime.transform.position, Quaternion.identity);

        slimeInfos.splits.Remove(actualSlime);

        split1.GetComponent<HealthBar>().healthBar = actualSlime.GetComponent<HealthBar>().healthBar;
        split2.GetComponent<HealthBar>().healthBar = actualSlime.GetComponent<HealthBar>().healthBar;

        split1.GetComponent<SlimeManager>().slimeInfos = actualSlime.GetComponent<SlimeManager>().slimeInfos;
        split2.GetComponent<SlimeManager>().slimeInfos = actualSlime.GetComponent<SlimeManager>().slimeInfos;
        
        split1.GetComponent<Split>().slimeInfos = actualSlime.GetComponent<Split>().slimeInfos;
        split2.GetComponent<Split>().slimeInfos = actualSlime.GetComponent<Split>().slimeInfos;

        split1.GetComponent<Split>().splitPrefab = actualSlime.GetComponent<Split>().splitPrefab;
        split2.GetComponent<Split>().splitPrefab = actualSlime.GetComponent<Split>().splitPrefab;

        Destroy(actualSlime);

        slimeInfos.splits.Add(split1);
        slimeInfos.splits.Add(split2);

        split1.GetComponent<SplitAI>().AIisActive = false;
        split1.transform.GetChild(2).gameObject.SetActive(true);
        slimeInfos.activeSplit = split1;
    }

    public void MergeSlime(GameObject split1, GameObject split2)
    {
        GameObject merged = Instantiate(splitPrefab, Vector3.Lerp(split1.transform.position, split2.transform.position, 0.5f), Quaternion.identity);
        slimeInfos.splits.Add(merged);
        slimeInfos.splits.Remove(split1);
        slimeInfos.splits.Remove(split2);
        Destroy (split1);
        Destroy(split2);

        merged.GetComponent<SplitAI>().AIisActive = false;
        merged.transform.GetChild(2).gameObject.SetActive(true);
        slimeInfos.activeSplit = merged;
    }

    public void SwitchSplit()
    {

    }
}
