using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SugenoFuzzy : MonoBehaviour
{
    public float kesehatanTanaman;
    public float _lembab;
    public float _subur;

    float suburRendah, suburSedang, suburTinggi;
    float lembabRendah, lembabSedang, lembabTinggi;

    float disjungsi_kesehatan_tanaman_rendah,
        disjungsi_kesehatan_tanaman_sedang,
        disjungsi_kesehatan_tanaman_tinggi;

    public float ref_rendah, ref_sedang, ref_tinggi;

    void Start()
    {
        _lembab = 4f;
        _subur = 8f;

        ref_rendah = 5;
        ref_sedang = 10;
        ref_tinggi = 20;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            fuzzifikasi(_subur, _lembab);
            hitung_disjungsi();
            defuzifikasi();
        }
    }

    public float fuzzifikasi_subur_rendah(float nilai)
    {
        float subur_rendah = 0;
        if (nilai >= 5)
        {
            subur_rendah = 0;
        } else if (nilai > 3 && nilai < 5)
        {
            subur_rendah = (5 - nilai) / 2;
        } else if (nilai <= 3 && nilai > 0)
        {
            subur_rendah = 1;
        }
        print("suburRendah = " + subur_rendah);

        return subur_rendah;
    }

    public float fuzzifikasi_lembab_rendah(float nilai)
    {
        float lembab_rendah = 0;
        if (nilai >= 5)
        {
            lembab_rendah = 0;
        }
        else if (nilai > 3 && nilai < 5)
        {
            lembab_rendah = (5 - nilai) / 2;
        }
        else if (nilai <= 3 && nilai > 0)
        {
            lembab_rendah = 1;
        }
        print("lembabRendah = " + lembab_rendah);

        return lembab_rendah;
    }

    public float fuzzifikasi_subur_sedang(float nilai)
    {
        float subur_sedang = 1;
        if (nilai >= 5 && nilai <= 6)
        {
            subur_sedang = 1;
        }
        else if (nilai > 3 && nilai < 5)
        {
            subur_sedang = (nilai - 3) / 2;
        }
        else if (nilai > 6 && nilai < 8)
        {
            subur_sedang = (8 - nilai) / 2;
        } else if (nilai <= 3 || nilai >= 8)
        {
            subur_sedang = 0;
        }
        print("suburSedang = " + subur_sedang);

        return subur_sedang;
    }

    public float fuzzifikasi_lembab_sedang(float nilai)
    {
        float lembab_sedang = 1;
        if (nilai >= 5 && nilai <= 6)
        {
            lembab_sedang = 1;
        }
        else if (nilai > 3 && nilai < 5)
        {
            lembab_sedang = (nilai - 3) / 2;
        }
        else if (nilai > 6 && nilai < 8)
        {
            lembab_sedang = (8 - nilai) / 2;
        }
        else if (nilai <= 3 || nilai >= 8)
        {
            lembab_sedang = 0;
        }
        print("lembabSedang = " + lembab_sedang);

        return lembab_sedang;
    }

    public float fuzzifikasi_subur_tinggi(float nilai)
    {
        float subur_tinggi = 0;
        if (nilai <= 3 && nilai > 0)
        {
            subur_tinggi = 0;
        }
        else if (nilai > 3 && nilai < 5)
        {
            subur_tinggi = (5 - nilai) / 2;
        }
        else if (nilai >= 5)
        {
            subur_tinggi = 1;
        }
        print("suburTinggi = " + subur_tinggi);

        return subur_tinggi;
    }

    public float fuzzifikasi_lembab_tinggi(float nilai)
    {
        float lembab_tinggi = 0;
         if (nilai <= 3 && nilai > 0)
         {
             lembab_tinggi = 0;
         }
         else if (nilai > 3 && nilai < 5)
         {
             lembab_tinggi = (5 - nilai) / 2;
         }
         else if (nilai >= 5)
         {
             lembab_tinggi = 1;
         }
        print("lembabTinggi = " + lembab_tinggi);

        return lembab_tinggi;
    }

    public void fuzzifikasi (float nilaiSubur, float nilaiLembab)
    {
        suburRendah = fuzzifikasi_subur_rendah(nilaiSubur);
        suburSedang = fuzzifikasi_subur_sedang(nilaiSubur);
        suburTinggi = fuzzifikasi_subur_tinggi(nilaiSubur);

        lembabRendah = fuzzifikasi_lembab_rendah(nilaiLembab);
        lembabSedang = fuzzifikasi_lembab_sedang(nilaiLembab);
        lembabTinggi = fuzzifikasi_lembab_tinggi(nilaiLembab);
    }

    public float hitung_fuzzy_konklusi (int no_rule)
    {
        float hasil = 0;
        switch (no_rule)
        {
            case 1:
                hasil = Mathf.Min(suburRendah, lembabRendah);
                break;
            case 2:
                hasil = Mathf.Min(suburRendah, lembabSedang);
                break;
            case 3:
                hasil = Mathf.Min(suburRendah, lembabTinggi);
                break;
            case 4:
                hasil = Mathf.Min(suburSedang, lembabRendah);
                break;
            case 5:
                hasil = Mathf.Min(suburSedang, lembabSedang);
                break;
            case 6:
                hasil = Mathf.Min(suburSedang, lembabTinggi);
                break;
            case 7:
                hasil = Mathf.Min(suburTinggi, lembabRendah);
                break;
            case 8:
                hasil = Mathf.Min(suburTinggi, lembabSedang);
                break;
            case 9:
                hasil = Mathf.Min(suburTinggi, lembabTinggi);
                break;
        }
        return hasil;
    }

    public float hitung_kesehatan_tanaman_rendah(float r1, float r2, float r3, float r4)
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

    public float hitung_kesehatan_tanaman_sedang(float r5, float r7)
    {
        float hasil;
        float[] list = new float[2];
        list[0] = r5;
        list[1] = r7;
        hasil = Mathf.Max(list);
        return hasil;
    }

    public float hitung_kesehatan_tanaman_tinggi(float r6, float r8, float r9)
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
        for (int i=0; i <=8; i++)
        {
            rule[i] = hitung_fuzzy_konklusi(i + 1);
        }
        disjungsi_kesehatan_tanaman_rendah = hitung_kesehatan_tanaman_rendah(rule[0], rule[1], rule[2], rule[3]);
        disjungsi_kesehatan_tanaman_sedang = hitung_kesehatan_tanaman_sedang(rule[4], rule[6]);
        disjungsi_kesehatan_tanaman_tinggi = hitung_kesehatan_tanaman_tinggi(rule[5], rule[7], rule[8]);

        print("disjungsi_kesehatan_tanaman_rendah = " + disjungsi_kesehatan_tanaman_rendah);
        print("disjungsi_kesehatan_tanaman_sedang = " + disjungsi_kesehatan_tanaman_sedang);
        print("disjungsi_kesehatan_tanaman_tinggi = " + disjungsi_kesehatan_tanaman_tinggi);
    }

    public float defuzifikasi()
    {
        float titikBerat;
        titikBerat = ((ref_tinggi * disjungsi_kesehatan_tanaman_tinggi) + (ref_sedang * disjungsi_kesehatan_tanaman_sedang) + (ref_rendah * disjungsi_kesehatan_tanaman_rendah));

        print("titik berat = " + titikBerat);
        return titikBerat;
    }
}
