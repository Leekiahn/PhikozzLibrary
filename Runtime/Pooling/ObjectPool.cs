using System;
using System.Collections.Generic;

namespace PhikozzLibrary.Runtime.Pooling
{
    public class ObjectPool<T> : IObjectPool<T> where T : class
    {
        #region >---------------------------------------------- Properties & Fields

        private readonly Stack<T> _pool = new Stack<T>();   // 오브젝트 스택
        private readonly Func<T> _createFunc;   // 오브젝트 생성 함수
        private readonly Action<T> _onSet;  // 오브젝트 반환 시 실행할 액션
        private readonly Action<T> _onGet;  // 오브젝트 꺼낼 때 실행할 액션

        #endregion

        #region >---------------------------------------------- Constructors

        public ObjectPool(Func<T> createFunc,Action<T> onSet = null, Action<T> onGet = null)
        {
            _createFunc = createFunc ?? throw new ArgumentNullException(nameof(createFunc));
            _onSet = onSet;
            _onGet = onGet;
        }

        #endregion

        #region >---------------------------------------------- Methods

        /// <summary>
        /// 오브젝트 넣기
        /// </summary>
        /// <param name="obj">넣을 오브젝트</param>
        public void Set(T obj)
        {
            _onSet?.Invoke(obj);
            _pool.Push(obj);
        }
        
        /// <summary>
        /// 오브젝트 꺼내기
        /// </summary>
        /// <returns>꺼낸 오브젝트</returns>
        public T Get()
        {
            var obj = _pool.Count > 0 ? _pool.Pop() : _createFunc();
            _onGet?.Invoke(obj);
            return obj;
        }

        /// <summary>
        /// 풀 비우기
        /// </summary>
        public void Clear()
        {
            _pool.Clear();
        }

        #endregion
    }
}
