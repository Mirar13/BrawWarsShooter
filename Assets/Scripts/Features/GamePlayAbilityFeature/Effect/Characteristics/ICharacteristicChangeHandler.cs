namespace DefaultNamespace.Characteristics
{
    public interface ICharacteristicChangeHandler
    {
        public void CharacteristicChanged(string type, float previousValue, float currentValue);
    }
}