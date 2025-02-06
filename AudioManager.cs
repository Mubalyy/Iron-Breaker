using UnityEngine;

// AudioManager to handle all game sounds
public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class SoundEffects
    {
        [Header("Player Sounds")]
        public AudioClip[] footsteps;
        public AudioClip[] attacks;
        public AudioClip impact;
        public AudioClip death;
        public AudioClip block;


        [Header("Enemy Sounds")]
        public AudioClip[] enemyFootsteps;
        public AudioClip[] enemyAttacks;
        public AudioClip enemyImpact;
        public AudioClip enemyDeath;
        public AudioClip enemyblock;
    }

    public static AudioManager Instance { get; private set; }
    [SerializeField] private SoundEffects sounds;
    [SerializeField] private float footstepInterval = 0.5f;
    
    private AudioSource[] audioSources;
    private float lastFootstepTime;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudioSources();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeAudioSources()
    {
        // Create multiple audio sources for overlapping sounds
        audioSources = new AudioSource[4];
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i] = gameObject.AddComponent<AudioSource>();
            audioSources[i].playOnAwake = false;
        }
    }

    private AudioSource GetAvailableAudioSource()
    {
        foreach (AudioSource source in audioSources)
        {
            if (!source.isPlaying)
                return source;
        }
        return audioSources[0]; // If all are playing, use the first one
    }

    // Player Sound Methods
    public void PlayFootstep(bool isPlayer, float volumeMultiplier = 1f)
    {
        if (Time.time - lastFootstepTime < footstepInterval) return;
        
        AudioClip[] steps = isPlayer ? sounds.footsteps : sounds.enemyFootsteps;
        if (steps.Length > 0)
        {
            AudioClip clip = steps[Random.Range(0, steps.Length)];
            AudioSource source = GetAvailableAudioSource();
            source.volume = 0.6f * volumeMultiplier;
            source.pitch = Random.Range(0.95f, 1.05f);
            source.PlayOneShot(clip);
        }
        lastFootstepTime = Time.time;
    }

    public void PlayAttack(bool isPlayer)
    {
        AudioClip[] attackSounds = isPlayer ? sounds.attacks : sounds.enemyAttacks;
        if (attackSounds.Length > 0)
        {
            AudioClip clip = attackSounds[Random.Range(0, attackSounds.Length)];
            AudioSource source = GetAvailableAudioSource();
            source.volume = 0.8f;
            source.PlayOneShot(clip);
        }
    }

    public void PlayImpact(bool isPlayer)
    {
        AudioClip clip = isPlayer ? sounds.impact : sounds.enemyImpact;
        if (clip != null)
        {
            AudioSource source = GetAvailableAudioSource();
            source.volume = 0.7f;
            source.PlayOneShot(clip);
        }
    }

    public void PlayDeath(bool isPlayer)
    {
        AudioClip clip = isPlayer ? sounds.death : sounds.enemyDeath;
        if (clip != null)
        {
            AudioSource source = GetAvailableAudioSource();
            source.volume = 1f;
            source.PlayOneShot(clip);
        }
    }

    public void PlayBlock(bool isPlayer)
    {
         AudioClip clip = isPlayer ? sounds.block : sounds.enemyblock;
            if (clip != null)
            {
            AudioSource source = GetAvailableAudioSource();
            source.volume = 1f;
            source.PlayOneShot(clip);
            }
    }
}


