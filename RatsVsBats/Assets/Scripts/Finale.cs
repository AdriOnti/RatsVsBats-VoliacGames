using System.Threading.Tasks;
using UnityEngine;

public class Finale : MonoBehaviour
{
    public int finalePoints;
    private async void OnTriggerEnter(Collider other)
    {
        FadeManager.Instance.FadeOut();
        await GetInfo();
        CanvasManager.Instance.Credits("TO BE CONTINUED");
    }

    private async Task GetInfo()
    {
        string response = await APIManager.instance.GetProfileWhereIdUserAsync(DataManager.Instance.profileId);
        if (string.IsNullOrEmpty(response))
        {
            return;
        }

        ProfileData profileData = DataManager.Instance.ProcessJSON<ProfileData>(response);
        profileData.points += finalePoints;
        profileData.completedBranches += 1;
        await APIManager.instance.FinaleUpdate(profileData);
    }
}
