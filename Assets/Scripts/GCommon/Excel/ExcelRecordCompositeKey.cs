namespace Excel
{
    public class ExcelRecordCompositeKey
    {
        protected volatile int _hashCode = 0;
        public override int GetHashCode()
        {
            //base.GetHashCode();
            string message = string.Format("Please implement {0} 's GetHashCode() in ExcelRecordKeyExtend.cs", GetType().Name);
            throw new System.NotImplementedException(message);
        }

        public void ResetHashCode()
        {
            _hashCode = 0;
        }
    }
}
