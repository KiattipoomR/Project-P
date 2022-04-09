using Crop;
using Manager;
using UnityEngine;

namespace Entity
{
    public class CropEntity : MonoBehaviour
    {
        [SerializeField] private CropData cropData;

        private SpriteRenderer _renderer;
        private int _currentCropStage;
        private int _growthDays;

        private void Awake()
        {
            _renderer = GetComponentInChildren<SpriteRenderer>();
        }

        public void Init(CropData crop)
        {
            if (!crop || crop.Stages.Count < 1) Destroy(gameObject);
            cropData = crop;

            _currentCropStage = 0;
            _growthDays = 0;
            SetCropStageSprite();

            TimeManager.OnDayChanged += Grow;
        }

        private void Grow()
        {
            if (_currentCropStage == cropData.Stages.Count - 1) return;

            _growthDays++;
            if (_growthDays < cropData.Stages[_currentCropStage].GrowthDays) return;

            _growthDays = 0;
            _currentCropStage++;

            SetCropStageSprite();
        }

        private void SetCropStageSprite()
        {
            _renderer.sprite = cropData.Stages[_currentCropStage].GrowthStageSprite;
        }
    }
}