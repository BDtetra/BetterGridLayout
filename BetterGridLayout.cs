using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BetterGridLayout
{
	public class BetterGridLayout : LayoutGroup
	{
		public enum EFitType
		{
			Uniform = 0,
			RowPriority,
			ColumnPriority,
			FixedRows,
			FixedColumns
		}

		public enum EAlignment
		{
			Horizontal = 0,
			Vertical,
		}

		public EAlignment alignment;
		public EFitType fitType;

		public int rows;
		public int columns;
		public Vector2 spacing;

		public bool autoFitX, autoFitY;
		public bool flipX, flipY;
		public float cellSizeX, cellSizeY;

		public bool freezeLayout;


		public override void CalculateLayoutInputVertical()
		{
			//throw new System.NotImplementedException();
		}

		public override void SetLayoutHorizontal()
		{
			// �Čv�Z�������΂�
			if (freezeLayout) return;

			base.CalculateLayoutInputHorizontal();

			// �͈͊O�ɐ��l�łȂ��悤�ɒ���
			if (rows < 1) rows = 1;
			if (columns < 1) columns = 1;
			if (cellSizeX <= 0) cellSizeX = 1f;
			if (cellSizeY <= 0) cellSizeY = 1f;

			int virtualRowsCount, virtualColumnCount;

			// �ʏ�z�u
			if (fitType == EFitType.RowPriority || fitType == EFitType.ColumnPriority || fitType == EFitType.Uniform)
			{
				autoFitX = true;
				autoFitY = true;

				float sqrt = Mathf.Sqrt(transform.childCount);
				rows = Mathf.Max(1, Mathf.CeilToInt(sqrt));
				columns = Mathf.Max(1, Mathf.CeilToInt(sqrt));
			}

			// ��̌v�Z
			if (fitType == EFitType.RowPriority || fitType == EFitType.FixedColumns || fitType == EFitType.Uniform)
			{
				rows = Mathf.CeilToInt(transform.childCount / (float)columns);
			}

			// �s�̌v�Z
			if (fitType == EFitType.ColumnPriority || fitType == EFitType.FixedRows || fitType == EFitType.Uniform)
			{
				columns = Mathf.CeilToInt(transform.childCount / (float)rows);
			}

			virtualRowsCount = rows;
			virtualColumnCount = columns;

			// sizeDelta�ł̓_���Astretch�ɂ���ƕ���邽��
			Vector2 parentSize = rectTransform.rect.size;

			// �Z���T�C�Y�̎�������
			// �w�蕝�̏ꍇ�͖��������
			if (autoFitX || autoFitY)
			{
				float width = (parentSize.x - spacing.x * (columns - 1) - padding.left - padding.right) / (float)columns;
				float height = (parentSize.y - spacing.y * (rows - 1) - padding.top - padding.bottom) / (float)rows;

				// ���C�A�E�g���Ԃɂ���ē���ւ��ēK��
				if (autoFitX)
				{
					cellSizeX = (alignment == EAlignment.Horizontal ? width : height);
				}
				if (autoFitY)
				{
					cellSizeY = (alignment == EAlignment.Horizontal ? height : width);
				}
			}

			// �Z���̔z�u
			int columnCount = 0, rowCount = 0;
			for (int i = 0; i < rectChildren.Count; i++)
			{
				var item = rectChildren[i];

				if (alignment == EAlignment.Horizontal)
				{
					rowCount = i / columns;
					columnCount = i % columns;

					var xPos = (cellSizeX + spacing.x) * (flipX ? (columns - columnCount - 1) : columnCount) + padding.left;
					var yPos = (cellSizeY + spacing.y) * (flipY ? (rows - rowCount - 1) : rowCount) + padding.top;

					SetChildAlongAxis(item, 0, xPos, cellSizeX);
					SetChildAlongAxis(item, 1, yPos, cellSizeY);
				}

				else
				{
					rowCount = i / rows;
					columnCount = i % rows;

					var xPos = (cellSizeX + spacing.x) * (flipX ? (columns - columnCount - 1) : columnCount) + padding.left;
					var yPos = (cellSizeY + spacing.y) * (flipY ? (rows - rowCount - 1) : rowCount) + padding.top;

					SetChildAlongAxis(item, 0, yPos, cellSizeY);
					SetChildAlongAxis(item, 1, xPos, cellSizeX);
				}
			}
		}

		public override void SetLayoutVertical()
		{
			//throw new System.NotImplementedException();
		}
	}
}