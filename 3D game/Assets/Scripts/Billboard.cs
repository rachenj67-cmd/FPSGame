using UnityEngine;

public class Billboard : MonoBehaviour
{
    // ใช้ LateUpdate เพื่อให้หลอดเลือดหมุนตามกล้องหลังจากกล้องขยับเสร็จแล้ว จะได้ไม่สั่น
    void LateUpdate()
    {
        if (Camera.main != null)
        {
            // สั่งให้ UI หันหน้าขนานกับหน้าจอกล้องตลอดเวลา
            transform.LookAt(transform.position + Camera.main.transform.forward);
        }
    }
}
