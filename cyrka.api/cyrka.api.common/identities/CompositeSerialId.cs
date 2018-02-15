using System;
using System.Linq;

namespace cyrka.api.common.identities
{
	public class CompositeSerialId
	{
		public ulong Serial { get { return (ulong)_idParts.Last(); } }

		public string CommonPrefix
		{
			get
			{
				var lInd = _value.LastIndexOf('-');
				return _value.Substring(0, lInd + 1);
			}
		}

		public CompositeSerialId(ulong serialNumber, params object[] prefixes)
		{
			CreateParts(serialNumber, prefixes);
			CreateValue();
		}

		public override string ToString()
		{
			return _value;
		}

		private object[] _idParts;
		private string _value;

		private void CreateParts(ulong serialNumber, object[] prefixes)
		{
			_idParts = new object[prefixes.Length + 1];
			Array.Copy(prefixes, _idParts, prefixes.Length);
			_idParts[prefixes.Length] = serialNumber;
		}

		private void CreateValue()
		{
			_value = string.Join('-', _idParts);
		}

		public static implicit operator string(CompositeSerialId compositeSerialId)
		{
			if (compositeSerialId == null)
				return null;
			return compositeSerialId.ToString();
		}

		public static implicit operator CompositeSerialId(string stringId)
		{
			if (string.IsNullOrEmpty(stringId))
				return null;
			ulong serial;
			var splittedId = stringId.Split('-');
			var lInd = splittedId.Length - 1;
			if (!ulong.TryParse(splittedId[lInd], out serial))
				throw new InvalidCastException($"Can't cast {stringId} to {nameof(CompositeSerialId)}");
			var prefixes = new string[lInd];
			Array.Copy(splittedId, prefixes, lInd);
			return new CompositeSerialId(serial, prefixes);
		}
	}
}
