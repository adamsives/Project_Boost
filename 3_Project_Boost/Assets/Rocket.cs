using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidbody;
    Transform transform;
    [SerializeField] float rcsThrust = 250f;
    [SerializeField] float rcsMainThrust = 50f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip deathExplosion;
    [SerializeField] AudioClip landingSuccess;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem deathExplosionParticles;
    [SerializeField] ParticleSystem landingSuccessParticles;

    enum State { Alive, Dying, Transcending}

    [SerializeField] State state = State.Alive;
    
    AudioSource audio;

    // Use this for initialization

    void Start()
    {
        //print("started!");
        rigidbody = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //todo stop sound on death
        if (state == State.Alive){
            Rotate();
            Thrust();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (state != State.Alive) {return;}

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }

    private void StartSuccessSequence()
    {
        state = State.Transcending;
        audio.Stop();
        audio.PlayOneShot(landingSuccess);
        landingSuccessParticles.Play();
        Invoke("LoadNextLevel", 1f);
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        audio.Stop();
        audio.PlayOneShot(deathExplosion);
        deathExplosionParticles.Play();
        Invoke("LoadFirstLevel", 1f);
        rigidbody.AddRelativeForce(Vector3.back);
    }

    private void LoadFirstLevel()
    {
        print("LoadFirstLevel");
        SceneManager.LoadScene(0);
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
    }

    private void Rotate()
    {
        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.back * rotationThisFrame);
        }

    }

    private void Thrust()
    {

        rigidbody.freezeRotation = true;

        float forwardThrustThisFrame = rcsMainThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust(forwardThrustThisFrame);
        }
        else
        {
            audio.Stop();
            mainEngineParticles.Stop();
        }
        rigidbody.freezeRotation = false;
    }

    private void ApplyThrust(float forwardThrustThisFrame)
    {
        rigidbody.AddRelativeForce(Vector3.up * forwardThrustThisFrame * Time.deltaTime);
        if (!audio.isPlaying)
        {
            audio.PlayOneShot(mainEngine);
        }

        mainEngineParticles.Play();

    }
}
