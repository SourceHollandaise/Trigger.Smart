
namespace XForms.Design
{

    public static class ScreenResolutionTypeCalculator
    {
        public static ScreenResolutionType GetScreenType()
        {
            var mainWidth = Eto.Forms.Application.Instance.MainForm.Screen.WorkingArea.Width;

            if (mainWidth <= 1024)
                return ScreenResolutionType.XtraSmall;

            if (mainWidth <= 1280)
                return ScreenResolutionType.Small;

            if (mainWidth <= 1440)
                return ScreenResolutionType.Medium;

            if (mainWidth < 1920)
                return ScreenResolutionType.Large;

            return ScreenResolutionType.XtraLarge;
        }

        public static int CalculatOptimumListDetailColumns(IListViewDescriptor descriptor)
        {
            var screenType = GetScreenType();

            switch (screenType)
            {
                case ScreenResolutionType.XtraSmall:
                    return 1;
                case ScreenResolutionType.Small:
                    if (descriptor.ListDetailViewColumns > 2)
                        return 2;
                    return descriptor.ListDetailViewColumns;
                case ScreenResolutionType.Medium:
                    return descriptor.ListDetailViewColumns - 1;
                case ScreenResolutionType.Large:
                    return descriptor.ListDetailViewColumns;
                case ScreenResolutionType.XtraLarge:
                    return descriptor.ListDetailViewColumns + 1;
                default:
                    throw new System.ArgumentOutOfRangeException();
            }

        }
    }
}
