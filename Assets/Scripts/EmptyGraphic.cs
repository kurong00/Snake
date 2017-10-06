using UnityEngine.UI;

namespace CnControls
{
	public class EmptyGraphic : Graphic
	{
		protected override void OnPopulateMesh(VertexHelper vh)
		{
			vh.Clear();
		}
	}
}
