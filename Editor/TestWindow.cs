using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public enum TestEnum
{
    OptionA, 
    OptionB, 
    OptionC
}

public class TestWindow : BaseWindowEditor
{
    private bool foldoutOpen = true;
    private string userName = "홍길동";
    private int userAge = 20;
    private float userHeight = 175.5f;
    private Vector2 vec2 = Vector2.one;
    private Vector3 vec3 = Vector3.one;
    private Vector4 vec4 = Vector4.one;
    private Rect rect = new Rect(0, 0, 10, 10);
    private RectInt rectInt = new RectInt(0, 0, 10, 10);
    private Color color = Color.white;
    private Bounds bounds = new Bounds(Vector3.zero, Vector3.one);
    private BoundsInt boundsInt = new BoundsInt(0, 0, 0, 1, 1, 1);
    private bool toggleValue = true;
    private TestScriptableObject testObj;
    private TestEnum testEnum = TestEnum.OptionA;
    
    // 클래스 필드에 스크롤 위치 변수 추가
    private Vector2 scrollPos;

    [MenuItem("PhikozzLibrary/Test Window")]
    public static void OpenWindow()
    {
        GetWindow<TestWindow>("Test Window");
    }

    protected override void OnGUI()
    {
        DrawScrollView(ref scrollPos, () =>
        {
            DrawLabel("BaseWindowEditor 메서드 예시");

            DrawLine();
            DrawSpace();

            DrawInfoBox("이것은 InfoBox입니다.");
            DrawWarningBox("이것은 WarningBox입니다.");
            DrawErrorBox("이것은 ErrorBox입니다.");

            DrawBox("이것은 단순 Box입니다.");

            DrawFoldout("Foldout 예시", ref foldoutOpen);
            if (foldoutOpen)
            {
                DrawField("이름", ref userName);
                DrawField("나이", ref userAge);
                DrawField("키", ref userHeight);
                DrawField("Vector2", ref vec2);
                DrawField("Vector3", ref vec3);
                DrawField("Vector4", ref vec4);
                DrawField("Rect", ref rect);
                DrawField("RectInt", ref rectInt);
                DrawField("Color", ref color);
                DrawField("Bounds", ref bounds);
                DrawField("BoundsInt", ref boundsInt);
                DrawEnumPopup("Enum", ref testEnum);
                DrawField("ScriptableObject", ref testObj);
                DrawToggle("토글", ref toggleValue, v => Debug.Log("토글 변경: " + v));
            }

            DrawSpace(10);

            DrawButton("버튼 (100x30)", 100, 30, new UnityAction(() => Debug.Log("버튼 클릭됨")));
        });
    }
}

// 예시용 ScriptableObject 클래스
public class TestScriptableObject : ScriptableObject { }
