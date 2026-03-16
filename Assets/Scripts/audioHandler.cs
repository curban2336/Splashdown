using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioHandler : MonoBehaviour
{
    public AudioClip splash;
    public AudioClip swimming;
    public AudioClip ambience;
    public AudioClip explosion;
    public AudioClip flying;
    public AudioClip sonar;
    public AudioClip boost;

    public AudioSource ambienceAS;
    public AudioSource flyingAS;
    public AudioSource swimmingAS;
    public AudioSource musicAS;

    // track previous states so one-shots only trigger on transitions
    private bool prevJumpStart = false;
    private bool prevIsJumping = false;

    void Awake()
    {
        // Ensure AudioSources exist (optional fallback)
        if (musicAS == null) musicAS = gameObject.AddComponent<AudioSource>();
        if (flyingAS == null) flyingAS = gameObject.AddComponent<AudioSource>();
        if (swimmingAS == null) swimmingAS = gameObject.AddComponent<AudioSource>();
        if (ambienceAS == null) ambienceAS = gameObject.AddComponent<AudioSource>();
    }

    void Start()
    {
        // configure loop-capable sources
        if (flyingAS != null) flyingAS.loop = true;
        if (swimmingAS != null) swimmingAS.loop = true;
        if (ambienceAS != null) ambienceAS.loop = true;
    }

    void Update()
    {
        // Play splash once when jumpStart becomes true (transition)
        if (BG_MoveLeft.jumpStart && !prevJumpStart)
        {
            if (musicAS != null && splash != null)
                musicAS.PlayOneShot(splash, 0.5f);
        }

        // If player is in-air (jumping) and not in "jumpStart" state, start flying loop and stop others
        if (ColorSwitcherWater.isJumping && !BG_MoveLeft.jumpStart)
        {
            // start flying loop if not already playing that clip
            if (flyingAS != null && flying != null)
            {
                if (flyingAS.clip != flying)
                {
                    flyingAS.clip = flying;
                    flyingAS.loop = true;
                }
                if (!flyingAS.isPlaying) flyingAS.Play();
            }

            if (swimmingAS != null) swimmingAS.Stop();
            if (ambienceAS != null) ambienceAS.Stop();
        }

        // When not jumping and not in jumpStart, play swimming/ambience loops
        if (!ColorSwitcherWater.isJumping && !BG_MoveLeft.jumpStart)
        {
            if (flyingAS != null) flyingAS.Stop();

            if (swimmingAS != null && swimming != null)
            {
                if (swimmingAS.clip != swimming)
                {
                    swimmingAS.clip = swimming;
                    swimmingAS.loop = true;
                }
                if (!swimmingAS.isPlaying) swimmingAS.Play();
            }

            if (ambienceAS != null && ambience != null)
            {
                if (ambienceAS.clip != ambience)
                {
                    ambienceAS.clip = ambience;
                    ambienceAS.loop = true;
                }
                if (!ambienceAS.isPlaying) ambienceAS.Play();
            }
        }

        // Example: play explosion once when some condition transitions (uncomment and adapt)
        // if (MineCode.Detonation && !prevDetonation) { musicAS.PlayOneShot(explosion, 1f); }

        // store previous states for transition detection
        prevJumpStart = BG_MoveLeft.jumpStart;
        prevIsJumping = ColorSwitcherWater.isJumping;
    }
}