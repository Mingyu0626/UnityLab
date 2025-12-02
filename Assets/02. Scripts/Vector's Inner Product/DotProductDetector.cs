using UnityEngine;

public class DotProductDetector : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] private Transform target;
    
    [Header("Detection Settings")]
    [SerializeField] private float detectionAngle = 90f;
    [SerializeField] private bool showDebugInfo = true;
    
    [Header("Visualization")]
    [SerializeField] private float lineLength = 2f;
    [SerializeField] private Color forwardColor = Color.blue;
    [SerializeField] private Color toTargetColor = Color.yellow;
    [SerializeField] private Color frontColor = Color.green;
    [SerializeField] private Color backColor = Color.red;
    
    private float dotProduct;
    private bool isInFront;
    
    void Update()
    {
        if (target == null)
        {
            Debug.LogWarning("Target이 설정되지 않았습니다!");
            return;
        }
        
        CalculateDotProduct();
        
        if (showDebugInfo)
        {
            DrawDebugLines();
            DisplayDebugInfo();
        }
    }
    
    /// <summary>
    /// 내적을 계산하여 목표물이 앞에 있는지 뒤에 있는지 판별
    /// </summary>
    void CalculateDotProduct()
    {
        // 캐릭터의 앞 방향 벡터
        Vector3 forward = transform.forward;
        
        // 캐릭터에서 목표물로 향하는 방향 벡터
        Vector3 toTarget = (target.position - transform.position).normalized;
        
        // 내적 계산
        // dot > 0: 목표물이 앞쪽에 위치
        // dot < 0: 목표물이 뒤쪽에 위치
        // dot = 0: 목표물이 정확히 옆에 위치
        dotProduct = Vector3.Dot(forward, toTarget);
        
        // 앞/뒤 판별
        isInFront = dotProduct > 0;
    }
    
    /// <summary>
    /// 디버그용 선 그리기
    /// </summary>
    void DrawDebugLines()
    {
        // 캐릭터의 앞 방향 표시 (파란색)
        Debug.DrawRay(transform.position, transform.forward * lineLength, forwardColor);
        
        // 캐릭터에서 목표물로 향하는 방향 표시 (노란색)
        Vector3 toTarget = (target.position - transform.position).normalized;
        Debug.DrawRay(transform.position, toTarget * lineLength, toTargetColor);
        
        // 목표물 위치에 표시 (앞: 초록색, 뒤: 빨간색)
        Debug.DrawLine(
            target.position + Vector3.up * 0.5f, 
            target.position - Vector3.up * 0.5f, 
            isInFront ? frontColor : backColor
        );
    }
    
    /// <summary>
    /// 화면에 디버그 정보 표시
    /// </summary>
    void DisplayDebugInfo()
    {
        string position = isInFront ? "앞쪽" : "뒤쪽";
        float angle = Mathf.Acos(dotProduct) * Mathf.Rad2Deg;
        
        Debug.Log($"[내적 판별] Dot Product: {dotProduct:F3} | 위치: {position} | 각도: {angle:F1}°");
    }
    
    /// <summary>
    /// 목표물이 특정 각도 내에 있는지 확인 (추가 기능)
    /// </summary>
    public bool IsTargetInViewAngle()
    {
        Vector3 forward = transform.forward;
        Vector3 toTarget = (target.position - transform.position).normalized;
        float dot = Vector3.Dot(forward, toTarget);
        
        // 내적 값을 각도로 변환하여 비교
        float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
        return angle <= detectionAngle;
    }
    
    // Public 접근자
    public float GetDotProduct() => dotProduct;
    public bool IsTargetInFront() => isInFront;
    
    void OnDrawGizmos()
    {
        if (target == null) return;
        
        // 캐릭터 위치에 작은 구 표시
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 0.3f);
        
        // 목표물 위치에 구 표시
        Gizmos.color = isInFront ? frontColor : backColor;
        Gizmos.DrawWireSphere(target.position, 0.3f);
        
        // 연결선
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, target.position);
    }
}