using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HamaScript : MonoBehaviour
{
    public Text belalangTxt;
    public Text tikusTxt;
    public Text hamaWarnTxt;

    public int _belalang;
    public int _tikus;
    public float tingkatHama;

    float belalangRendah, belalangSedang, belalangTinggi;
    float tikusRendah, tikusSedang, tikusTinggi;

    float disjungsi_tingkat_hama_rendah, 
        disjungsi_tingkat_hama_sedang, 
        disjungsi_tingkat_hama_tinggi;

    int hasilDefuzzifikasi;

    public void hamaRandomNumb()
    {
        _belalang = Random.Range(1, 100);
        _tikus = Random.Range(1, 20);
    }

    public void hamaSetText()
    {
        belalangTxt.text = "Jumlah Belalang : " +_belalang;
        tikusTxt.text = "Jumlah Tikus : " +_tikus;
    }

    public void warnText()
    {
        if (hasilDefuzzifikasi == 0)
        {
            hamaWarnTxt.text = "LEVEL HAMA : TIDAK ADA";
        } else if (hasilDefuzzifikasi == 1)
        {
            hamaWarnTxt.text = "LEVEL HAMA : RENDAH";
        } else if (hasilDefuzzifikasi == 2)
        {
            hamaWarnTxt.text = "LEVEL HAMA : SEDANG";
        } else if (hasilDefuzzifikasi == 3)
        {
            hamaWarnTxt.text = "LEVEL HAMA : TINGGI";
        } else if (hasilDefuzzifikasi == 99)
        {
            hamaWarnTxt.text = "TANAMAN MATI";
        }
    }

    public void dayChange()
    {
        hamaRandomNumb();
        hamaSetText();
        fuzzifikasi(_belalang, _tikus);
        hitung_disjungsi();
        defuzifikasi();
    }

    // Start is called before the first frame update
    void Start()
    {
        GantiHari.changeEvent += dayChange;
        hamaRandomNumb();
        hamaSetText();
        fuzzifikasi(_tikus, _tikus);
        hitung_disjungsi();
        defuzifikasi();
    }

    // Update is called once per frame
    void Update()
    {
        warnText();
    }

    public float fuzzifikasi_belalang_rendah(float nilai)
    {
        float belalang_rendah = 0;
        if (nilai >= 50)
        {
            belalang_rendah = 0;
        }
        else if (nilai > 30 && nilai < 50)
        {
            belalang_rendah = (50 - nilai) / 20;
        }
        else if (nilai <= 30 && nilai > 0)
        {
            belalang_rendah = 1;
        }
        print("belalangRendah = " + belalang_rendah);

        return belalang_rendah;
    }

    public float fuzzifikasi_tikus_rendah(float nilai)
    {
        float tikus_rendah = 0;
        if (nilai >= 10)
        {
            tikus_rendah = 0;
        }
        else if (nilai > 6 && nilai < 10)
        {
            tikus_rendah = (10 - nilai) / 4;
        }
        else if (nilai <= 6 && nilai > 0)
        {
            tikus_rendah = 1;
        }
        print("tikusRendah = " + tikus_rendah);

        return tikus_rendah;
    }

    public float fuzzifikasi_belalang_sedang(float nilai)
    {
        float belalang_sedang = 1;
        if (nilai >= 50 && nilai <= 60)
        {
            belalang_sedang = 1;
        }
        else if (nilai > 30 && nilai < 50)
        {
            belalang_sedang = (nilai - 30) / 20;
        }
        else if (nilai > 60 && nilai < 80)
        {
            belalang_sedang = (80 - nilai) / 20;
        }
        else if (nilai <= 30 || nilai >= 80)
        {
            belalang_sedang = 0;
        }
        print("belalangSedang = " + belalang_sedang);

        return belalang_sedang;
    }

    public float fuzzifikasi_tikus_sedang(float nilai)
    {
        float tikus_sedang = 1;
        if (nilai >= 10 && nilai <= 12)
        {
            tikus_sedang = 1;
        }
        else if (nilai > 6 && nilai < 10)
        {
            tikus_sedang = (nilai - 6) / 4;
        }
        else if (nilai > 12 && nilai < 16)
        {
            tikus_sedang = (16 - nilai) / 4;
        }
        else if (nilai <= 6 || nilai >= 16)
        {
            tikus_sedang = 0;
        }
        print("tikusSedang = " + tikus_sedang);

        return tikus_sedang;
    }

    public float fuzzifikasi_belalang_tinggi(float nilai)
    {
        float belalang_tinggi = 0;
        if (nilai <= 60 && nilai > 0)
        {
            belalang_tinggi = 0;
        }
        else if (nilai > 60 && nilai < 80)
        {
            belalang_tinggi = (60 - nilai) / 20;
        }
        else if (nilai >= 60)
        {
            belalang_tinggi = 1;
        }
        print("belalangTinggi = " + belalang_tinggi);

        return belalang_tinggi;
    }

    public float fuzzifikasi_tikus_tinggi(float nilai)
    {
        float tikus_tinggi = 0;
        if (nilai <= 12 && nilai > 0)
        {
            tikus_tinggi = 0;
        }
        else if (nilai > 12 && nilai < 16)
        {
            tikus_tinggi = (12 - nilai) / 4;
        }
        else if (nilai >= 16)
        {
            tikus_tinggi = 1;
        }
        print("tikusTinggi = " + tikus_tinggi);

        return tikus_tinggi;
    }

    public void fuzzifikasi (float nilaiBelalang, float nilaiTikus)
    {
        belalangRendah = fuzzifikasi_belalang_rendah(nilaiBelalang);
        belalangSedang = fuzzifikasi_belalang_sedang(nilaiBelalang);
        belalangTinggi = fuzzifikasi_belalang_tinggi(nilaiBelalang);

        tikusRendah = fuzzifikasi_tikus_rendah(nilaiTikus);
        tikusSedang = fuzzifikasi_tikus_sedang(nilaiTikus);
        tikusTinggi = fuzzifikasi_tikus_tinggi(nilaiTikus);
    }

    public float hitung_fuzzy_konklusi(int no_rule)
    {
        float hasil = 0;
        switch (no_rule)
        {
            case 1:
                hasil = Mathf.Min(belalangRendah, tikusRendah);
                break;
            case 2:
                hasil = Mathf.Min(belalangRendah, tikusSedang);
                break;
            case 3:
                hasil = Mathf.Min(belalangRendah, tikusTinggi);
                break;
            case 4:
                hasil = Mathf.Min(belalangSedang, tikusRendah);
                break;
            case 5:
                hasil = Mathf.Min(belalangSedang, tikusSedang);
                break;
            case 6:
                hasil = Mathf.Min(belalangSedang, tikusTinggi);
                break;
            case 7:
                hasil = Mathf.Min(belalangTinggi, tikusRendah);
                break;
            case 8:
                hasil = Mathf.Min(belalangTinggi, tikusSedang);
                break;
            case 9:
                hasil = Mathf.Min(belalangTinggi, tikusTinggi);
                break;
        }
        return hasil;
    }

    public float hitung_tingkat_hama_rendah(float r1, float r2, float r3, float r4)
    {
        float hasil;
        float[] list = new float[4];
        list[0] = r1;
        list[1] = r2;
        list[2] = r3;
        list[3] = r4;
        hasil = Mathf.Max(list);
        return hasil;
    }

    public float hitung_tingkat_hama_sedang(float r5, float r7)
    {
        float hasil;
        float[] list = new float[2];
        list[0] = r5;
        list[1] = r7;
        hasil = Mathf.Max(list);
        return hasil;
    }

    public float hitung_tingkat_hama_tinggi(float r6, float r8, float r9)
    {
        float hasil;
        float[] list = new float[3];
        list[0] = r6;
        list[1] = r8;
        list[2] = r9;
        hasil = Mathf.Max(list);
        return hasil;
    }

    public void hitung_disjungsi()
    {
        float[] rule = new float[9];
        for (int i = 0; i <= 8; i++)
        {
            rule[i] = hitung_fuzzy_konklusi(i + 1);
        }
        disjungsi_tingkat_hama_rendah = hitung_tingkat_hama_rendah(rule[0], rule[1], rule[2], rule[3]);
        disjungsi_tingkat_hama_sedang = hitung_tingkat_hama_sedang(rule[4], rule[6]);
        disjungsi_tingkat_hama_tinggi = hitung_tingkat_hama_tinggi(rule[5], rule[7], rule[8]);

        print("disjungsi_tingkat_hama_rendah = " + disjungsi_tingkat_hama_rendah);
        print("disjungsi_tingkat_hama_sedang = " + disjungsi_tingkat_hama_sedang);
        print("disjungsi_tingkat_hama_tinggi = " + disjungsi_tingkat_hama_tinggi);
    }

    public float defuzifikasi()
    {
        float titikBerat;
        titikBerat = Mathf.Max(disjungsi_tingkat_hama_rendah, disjungsi_tingkat_hama_sedang, disjungsi_tingkat_hama_tinggi);

        if (titikBerat == disjungsi_tingkat_hama_rendah)
        {
            hasilDefuzzifikasi = 1;
        } else if (titikBerat == disjungsi_tingkat_hama_sedang)
        {
            hasilDefuzzifikasi = 2;
        } else if (titikBerat == disjungsi_tingkat_hama_tinggi)
        {
            hasilDefuzzifikasi = 3;
        }
        print("hasil Defizzifikasi = " +hasilDefuzzifikasi);
        return titikBerat;
    }

    public void PestisidaBenar ()
    {
        _belalang = 0;
        _tikus = 0;
        hamaSetText();
        hasilDefuzzifikasi = 0;
    }

    public void PestisidaKurang()
    {
        print("tidak berpengaruh");
    }

    public void PestisidaLebih()
    {
        _belalang = 0;
        _tikus = 0;
        hamaSetText();
        hasilDefuzzifikasi = 99;
    }

    public void PestisidaButton(Button btn)
    {
        if (btn.name == "PestisidaRendahButton")
        {
            if (hasilDefuzzifikasi == 1)
            {
                PestisidaBenar();
            } else if (hasilDefuzzifikasi == 2 || hasilDefuzzifikasi == 3)
            {
                PestisidaKurang();
            }
        }
        else if (btn.name == "PestisidaSedangButton")
        {
            if (hasilDefuzzifikasi == 2)
            {
                PestisidaBenar();
            } else if (hasilDefuzzifikasi == 1)
            {
                PestisidaLebih();
            } else if (hasilDefuzzifikasi == 3)
            {
                PestisidaKurang();
            }
        }
        else if (btn.name == "PestisidaTinggiButton")
        {
            if (hasilDefuzzifikasi == 3)
            {
                PestisidaBenar();
            } else if (hasilDefuzzifikasi == 2 || hasilDefuzzifikasi == 1)
            {
                PestisidaLebih();
            }
        }
    }
}
