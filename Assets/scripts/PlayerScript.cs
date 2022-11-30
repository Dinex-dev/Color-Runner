using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using Random = UnityEngine.Random;


public class PlayerScript : MonoBehaviour
{
    public GameObject cam;
    public Vector3 offset;
    public Material[] materialsArray;
    public TextMeshProUGUI ScoreText;
    private Rigidbody rb;
    private float speed= 20f;
    public GameObject obstacle;
    private GameObject obstacleClone , ground ;
    private GameObject[] objects ,obstacleList;
    private int Score = 0;
    public AudioClip ding;
    public Canvas GameOver;
    Material RandomMaterial(Material[] materials)
    {
        return materials[Random.Range(0, materials.Length)];
    }
    
    void Start()
    {
        this.GetComponent<MeshRenderer>().material = RandomMaterial(materialsArray);
        GetComponent<AudioSource> ().playOnAwake = false;
        GetComponent<AudioSource> ().clip = ding;
        
    }

    void MakeGround(Collider other)
    {
        ground = Instantiate(other.gameObject, new Vector3(0,0,other.gameObject.transform.position.z+50), Quaternion.identity);
        objects = GameObject.FindGameObjectsWithTag("ground");
        foreach (var obj in objects)
        {
            if (obj.transform.position.z - this.gameObject.transform.position.z < -100)
            {
                Destroy(obj);
            }
        }

        obstacleList = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (var obj in obstacleList)
        {
            if (obj.transform.position.z - this.gameObject.transform.position.z < -100)
            {
                Destroy(obj);
            }
        }

        for (int i = 0; i < 5; i++)
        {
            obstacleClone = Instantiate(obstacle, new Vector3(Random.Range(-3.5f,3.5f),0.5f,ground.gameObject.transform.position.z+10*i), Quaternion.identity);
            obstacleClone.gameObject.GetComponent<MeshRenderer>().material = RandomMaterial(materialsArray);
        }
    }

    private void CheckObstacle(Collider other)
    {
        
        if(other.gameObject.GetComponent<MeshRenderer>().material.name == this.gameObject.GetComponent<MeshRenderer>().material.name)
        {
            Destroy(other.gameObject);
            this.GetComponent<MeshRenderer>().material = RandomMaterial(materialsArray);
            ScoreText.text = "Score : " + ++Score;
            GetComponent<AudioSource> ().Play ();
        }
        else
        {
            GameOver.gameObject.SetActive(true);
            Destroy(this.gameObject);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            MakeGround(other);
        }
        if (other.gameObject.CompareTag("Obstacle"))
        {
            CheckObstacle(other);
        }
    }
    
    private void FixedUpdate()
    {
        rb = this.GetComponent<Rigidbody>();
        float xMove = Input.GetAxisRaw("Horizontal");
        if (rb.velocity.z < 10f)
        {
            rb.velocity = new Vector3(0,0,10);
        }
        
        rb.AddForce(new Vector3(xMove*speed,0,0));
        cam.transform.position = this.transform.position + offset;
    }
    
}
