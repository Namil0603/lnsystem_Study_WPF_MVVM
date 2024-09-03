using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace lnsystem_Study03_ICD_ItemTemplate_.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        // 속성 값이 변경되었음을 알리는 이벤트
        public event PropertyChangedEventHandler? PropertyChanged;

        // 속성 값이 변경되었을 때 호출되는 메서드
        // [CallerMemberName] 어트리뷰트를 사용하여 호출된 속성의 이름을 자동으로 가져옴
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            // PropertyChanged 이벤트가 null이 아닌 경우에만 이벤트를 발생시킴
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // 속성 값을 설정하고 변경되었음을 알리는 메서드
        // 제네릭 타입 T를 사용하여 다양한 타입의 속성에 대해 사용 가능
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            // 현재 필드 값과 새로운 값이 같은지 비교
            if (EqualityComparer<T>.Default.Equals(field, value)) return false; // 같으면 false 반환

            // 필드 값을 새로운 값으로 설정
            field = value;

            // 속성 값이 변경되었음을 알림
            OnPropertyChanged(propertyName);

            // 값이 변경되었음을 나타내기 위해 true 반환
            return true;
        }
    }
}