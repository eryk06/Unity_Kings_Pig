// Trong script qu?n l? bom
using UnityEngine;

public Animator bombAnimator; // Tham chi?u ð?n Animator Controller c?a qu? bom

void Explode()
{
    // Kích ho?t animation n?
    bombAnimator.SetTrigger("ExplodeTrigger");

    // ... Các x? l? khác khi bom n? ...
}