using System;
using System.Windows.Forms;

namespace GameCaro
{
    public partial class hd : Form
    {
        public hd()
        {
            InitializeComponent();

            // Thiết lập nội dung TextBox khi khởi tạo Form
            textBox1.Text = "Luật chơi cơ bản của cờ caro:\r\n\r\n" +
                            "1. Cờ caro là một trò chơi hai người, sử dụng quân cờ đen và quân cờ trắng.\r\n\r\n" +
                            "2. Bàn cờ thường luật này áp dụng cho bất kỳ lưới nào.\r\n\r\n" +
                            "3. Mục tiêu của mỗi người chơi là đạt được 5 quân cờ liên tiếp trên cùng một hàng, cột, hoặc đường chéo.\r\n\r\n" +
                            "4. Hai người chơi luân phiên đặt một quân cờ vào giao điểm của các đường kẻ trên bàn cờ.\r\n\r\n" +
                            "5. Người đầu tiên xếp được 5 quân cờ liên tiếp trên cùng một hàng, cột, hoặc đường chéo sẽ thắng cuộc.\r\n\r\n" +
                            "6. Trò chơi có thể kết thúc hòa nếu bàn cờ đã kín mà không có ai đạt được 5 quân cờ liên tiếp.\r\n\r\n" +
                            "Chúc bạn chơi vui vẻ!";
        }

        // Sự kiện này không cần thay đổi nội dung của TextBox nữa
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Bạn có thể bỏ trống hoặc sử dụng sự kiện này cho các mục đích khác
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Ẩn form hiện tại
            this.Hide();

            // Mở form 'home'
            home hm = new home();
            hm.ShowDialog();

            // Sau khi form 'home' đóng, hủy đối tượng 'home' và đóng form hiện tại
            hm = null;
            this.Close();
        }
    }
}
