using System.ComponentModel;
using System.Runtime.InteropServices;

namespace CustomSuggest
{
    public class サジェストアイテム
    {
        public string ID { get; set; } = string.Empty;
        public string 名称 { get; set; } = string.Empty;
        public string 補助検索キー { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{ID} {名称}";
        }
    }

    public partial class ImeSuggestTextBox : UserControl
    {

        // プレースホルダー用 Win32 API
        private const int EM_SETCUEBANNER = 0x1501;
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, string lParam);

        private readonly ImeTextBox _textBox;
        private readonly ListBox _listBox;
        private readonly Label _labelID;

        private List<サジェストアイテム> _候補リスト = [];
        private int _最大表示件数 = 10;
        private int _textWidth = 100;
        private string _placeholderText = string.Empty;

        // -------------------------------------------------------
        // プロパティ
        // -------------------------------------------------------
        // サジェスト最大表示件数
        [Category("外観"), Description("サジェスト最大表示件数")]
        [DefaultValue(10)]
        public int 最大表示件数
        {
            get => _最大表示件数;
            set => _最大表示件数 = value > 0 ? value : 10;
        }

        public int リスト幅 => this.Width;
        // リストの高さはカスタムコントロールの高さ依存
        public int リスト高さ => this.Height - _textBox.Bottom;

        // テキストボックスの幅
        [Category("外観"), Description("テキストボックスの幅")]
        [DefaultValue(100)]
        public int TextWidth
        {
            get => _textWidth;
            set
            {
                if (value > 0)
                {
                    _textWidth = value;
                }
            }
        }

        // プレースホルダーテキスト
        [Category("外観"), Description("プレースホルダーテキスト")]
        [DefaultValue("")]
        public string Placeholder
        {
            get => _placeholderText;
            set
            {
                _placeholderText = value ?? string.Empty;
                SetPlaceholder();
            }
        }

        // Text
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Text
        {
            get => _textBox.Text ?? string.Empty;
#pragma warning disable CS8765
            set => _textBox.Text = value ?? string.Empty;
#pragma warning restore CS8765
        }

        // 候補リスト
        [Category("サジェスト候補リスト"), Description("サジェスト候補リスト")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<サジェストアイテム> 候補リスト
        {
            get => _候補リスト;
            set => _候補リスト = value ?? new List<サジェストアイテム>();
        }

        // 選択されたID
        [Category("結果"), Description("選択されたID")]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ID { get; private set; } = string.Empty;


        // -------------------------------------------------------
        // コンストラクタ
        // -------------------------------------------------------
        public ImeSuggestTextBox()
        {
            InitializeComponent();

            _textBox = new ImeTextBox
            {
                Location = new Point(0, 0),
                Anchor = AnchorStyles.Top | AnchorStyles.Left,
            };

            _labelID = new Label
            {
                AutoSize = true,
                BorderStyle = BorderStyle.None,
                TextAlign = ContentAlignment.MiddleLeft,
                Anchor = AnchorStyles.Top | AnchorStyles.Left,
                ForeColor = Color.Gray,
                BackColor = Color.Transparent,
            };

            _listBox = new ListBox
            {
                Visible = false,
                Anchor = AnchorStyles.Top | AnchorStyles.Left,
            };

            this.Controls.Add(_labelID);
            this.Controls.Add(_textBox);
            this.Controls.Add(_listBox);

            _listBox.Click += ListBox_Click;
            _textBox.KeyDown += TextBox_KeyDown;
            _textBox.Ime変換確定後 += TextBox_Ime変換確定後;
            _textBox.TextChanged += TextBox_TextChanged;
            _textBox.LostFocus += TextBox_LostFocus;
            _listBox.LostFocus += ListBox_LostFocus;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateLayout();
            SetPlaceholder();
        }

        // ---------------------------
        // 初期配置は以下の通り。
        // (ID)
        // 入力テキストボックス
        // ---------------------------
        private void UpdateLayout()
        {
            _textBox.Width = _textWidth;
            _labelID.Width = _textBox.Width;
            _labelID.Location = new Point(0, 0);
            _textBox.Location = new Point(0, _labelID.Bottom);

            _listBox.Width = リスト幅;
            _listBox.Height = リスト高さ > 0 ? リスト高さ : 0;
            _listBox.Location = new Point(_textBox.Left, _textBox.Bottom);
        }

        // プレースホルダー設定
        private void SetPlaceholder()
        {
            if (_textBox.IsHandleCreated)
            {
                SendMessage(_textBox.Handle, EM_SETCUEBANNER, (IntPtr)1, _placeholderText);
            }
        }

        // -------------------------------------------------------
        // イベントメソッド
        // -------------------------------------------------------
        // 半角文字入力後
        private void TextBox_TextChanged(object? sender, EventArgs e)
        {
            // IME入力中はスルー
            if (InputLanguage.CurrentInputLanguage.LayoutName.Contains("Japanese"))
                return;

            ID = string.Empty;
            UpdateIDLabel();
            Updateサジェスト();
        }

        // 日本語入力確定後
        private void TextBox_Ime変換確定後(object? sender, EventArgs e)
        {
            ID = string.Empty;
            UpdateIDLabel();
            Updateサジェスト();
        }

        private void Updateサジェスト()
        {
            _listBox.Visible = false;

            string 入力値 = _textBox.Text;
            if (string.IsNullOrWhiteSpace(入力値))
            {
                return;
            }

            var マッチ = _候補リスト
                .Where(x =>
                    x.ID.Contains(入力値) ||
                    x.名称.Contains(入力値) ||
                    x.補助検索キー.Contains(入力値))
                .Take(最大表示件数)
                .ToList();

            if (マッチ.Count == 0)
            {
                return;
            }

            _listBox.DataSource = null;
            _listBox.DataSource = マッチ;
            _listBox.DisplayMember = nameof(サジェストアイテム.ToString);

            _listBox.Visible = true;
        }

        // リストボックス選択後
        private void ListBox_Click(object? sender, EventArgs e)
        {
            if (_listBox.SelectedItem is サジェストアイテム item)
            {
                _textBox.Text = item.名称;
                ID = item.ID;
                UpdateIDLabel();
                _listBox.Visible = false;
                _textBox.Focus();
                _textBox.SelectionStart = _textBox.Text.Length;
            }
        }

        // テキストボックス キー操作（↓、↑、Enter、ESC）
        private void TextBox_KeyDown(object? sender, KeyEventArgs e)
        {
            if (!_listBox.Visible)
                return;

            if (e.KeyCode == Keys.Down)
            {
                if (_listBox.SelectedIndex < _listBox.Items.Count - 1)
                    _listBox.SelectedIndex++;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (_listBox.SelectedIndex > 0)
                    _listBox.SelectedIndex--;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                ListBox_Click(sender, EventArgs.Empty);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                _listBox.Visible = false;
                e.Handled = true;
            }
        }

        private void UpdateIDLabel()
        {
            if (string.IsNullOrEmpty(ID))
            {
                _labelID.Text = string.Empty;
                return;
            }
            _labelID.Text = $"({ID})";
        }

        // フォーカスが外れた場合はリスト閉じる
        private void TextBox_LostFocus(object? sender, EventArgs e)
        {
            // リストボックスにフォーカスが移った場合は閉じない
            if (!this.ContainsFocus)
            {
                _listBox.Visible = false;
            }
        }
        private void ListBox_LostFocus(object? sender, EventArgs e)
        {
            // どちらにもフォーカスがない場合はリスト閉じる
            if (!this.ContainsFocus)
            {
                _listBox.Visible = false;
            }
        }
    }
}
