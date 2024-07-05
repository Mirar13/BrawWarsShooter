namespace Features.GamePlayAbilityFeature.Components.PhysicsCast.Interfaces
{
    public interface IArrayContainerGetter<TData>
    {
        public bool Get<TData>(out TData result) where TData : struct;
    }
}