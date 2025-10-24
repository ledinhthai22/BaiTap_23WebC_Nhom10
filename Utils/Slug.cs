using System.Text;
using System.Text.RegularExpressions;

namespace BaiTap_23WebC_Nhom10.Utils
{
    public static class SlugHelper
    {
        public static string GenerateSlug(string phrase)
        {
            // 1. Kiểm tra đầu vào rỗng/null
            if (string.IsNullOrWhiteSpace(phrase))
            {
                return string.Empty;
            }

            // 2. Chuyển sang chữ thường
            string str = phrase.ToLowerInvariant();

            // 3. Xóa dấu tiếng Việt (Hàm của bạn đã đúng)
            str = RemoveVietnameseDiacritics(str);

            // 4. Xóa ký tự đặc biệt (chỉ giữ lại a-z, 0-9, khoảng trắng và gạch ngang)
            // LƯU Ý: Đã bao gồm cả dấu gạch ngang '-' trong Regex để tránh bị loại bỏ
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");

            // 5. Thay khoảng trắng bằng dấu gạch ngang và loại bỏ khoảng trắng dư thừa
            str = Regex.Replace(str, @"\s+", "-");

            // 6. Gộp nhiều dấu '-' liên tiếp thành 1 và loại bỏ '-' ở đầu/cuối
            str = Regex.Replace(str, @"-+", "-").Trim('-');

            // 7. Giới hạn độ dài slug (tùy chọn, nên có để tránh URL quá dài)
            if (str.Length > 100)
            {
                str = str.Substring(0, 100);
                // Sau khi cắt, trim lại lần cuối để tránh kết thúc bằng dấu '-'
                str = str.TrimEnd('-');
            }

            return str;
        }

        private static string RemoveVietnameseDiacritics(string text)
        {
            // (Giữ nguyên hàm của bạn - đã hoạt động tốt cho mục đích này)
            string[] vietnameseSigns = new string[]
            {
                "aAeEoOuUiIdDyY",
                "áàạảãâấầậẩẫăắằặẳẵ",
                "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
                "éèẹẻẽêếềệểễ",
                "ÉÈẸẺẼÊẾỀỆỂỄ",
                "óòọỏõôốồộổỗơớờợởỡ",
                "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
                "úùụủũưứừựửữ",
                "ÚÙỤỦŨƯỨỪỰỬỮ",
                "íìịỉĩ",
                "ÍÌỊỈĨ",
                "đ",
                "Đ",
                "ýỳỵỷỹ",
                "ÝỲỴỶỸ"
            };

            for (int i = 1; i < vietnameseSigns.Length; i++)
            {
                for (int j = 0; j < vietnameseSigns[i].Length; j++)
                    text = text.Replace(vietnameseSigns[i][j], vietnameseSigns[0][i - 1]);
            }

            return text;
        }
    }
}