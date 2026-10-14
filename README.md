"# UTS-Teknologi-Game" 

UTS Teknologi Game Komputerr
Naufal Mochtar 2305541055

1.	Arena & AI Musuh (NavMesh):
    a.	Buat sebuah lantai arena sederhana yang memiliki rintangan (Tembok/Rintangan).
    b.	Lakukan Bake pada arena tersebut menggunakan NavMesh.
    c.	Buat sebuah objek Musuh (Cylinder/Capsule) yang secara otomatis bergerak mengejar posisi Player menggunakan NavMeshAgent.
2.	Sistem Menembak Player (Raycasting):
    a.	Player dapat menembak dengan klik kiri mouse menggunakan Raycast.
    b.	Tembakan Raycast harus mendeteksi objek musuh, menghitung jarak tembak (hit.distance), dan mencetaknya di Console (misal: "Kena Musuh pada jarak 7.5 meter").
    c.	Jika Raycast mengenai musuh, HP musuh berkurang. Jika HP musuh habis (<= 0), musuh hancur (Destroy).
