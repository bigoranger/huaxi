using System;
using System.Text;
using System.Web.Mvc;

namespace HtmlPaginationBar
{
    #region Mvc 分页栏扩展方法
    /// <summary>
    ///  Mvc 分页栏扩展方法
    /// </summary>
    public static class HtmlPaginationBar
    {
        /// <summary>
        /// 生成分页栏（页面调用 @Html.PaginationBar）
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="bar">分页栏生成器</param>
        /// <returns></returns>
        public static MvcHtmlString PaginationBar(this HtmlHelper helper, PaginationBarBilder bar)
        {
            return new MvcHtmlString(bar.GenPaginationHtml());
        }
    }

    #endregion

    #region 分页条参数
    /// <summary>
    /// 分页条参数
    /// </summary>
    public class PageBarPars
    {
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { set; get; }
        /// <summary>
        /// 当前页索引
        /// </summary>
        public int PageIndex { set; get; }
    }
    #endregion

    #region 分页导航栏生成器
    public class PaginationBarBilder
    {
        /// <summary>
        /// 分页导航栏生成器
        /// </summary>
        /// <param name="url">页面地址模板，其中改变的页面索引部分使用{0}替换</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="pageIndex">当前页索引</param>
        public PaginationBarBilder(string url, int pageCount, int pageIndex)
        {
            m_url = url;
            m_pageCount = pageCount;
            m_pageIndex = pageIndex;
        }

        private int m_pageIndex = 0;
        private int m_pageCount = 0;
        private string m_url = string.Empty;
        private int m_offset = 3;
        private StringBuilder m_html = new StringBuilder();

        /// <summary>
        /// 生成分页m_html代码
        /// </summary>
        /// <returns></returns>
        public string GenPaginationHtml()
        {
            StringBuilder m_html = new StringBuilder();
            m_html.Append("<ul class='pagination'>");
            m_html.Append(GenPrevious());
            m_html.Append(GenLeftAnchor());
            m_html.Append(GenMidArea());
            m_html.Append(GenRightAnchor());
            m_html.Append(GenNext());
            m_html.Append("</ul>");
            return m_html.ToString();
        }
        /// <summary>
        /// 生成上一页
        /// </summary>
        private string GenPrevious()
        {
            StringBuilder m_html = new StringBuilder();
            string href = string.Empty;
            if (m_pageIndex <= 1 || m_pageCount <= 1)
            {
                m_html.Append("<li class='disabled'><a>&laquo;</a></li>");
                return m_html.ToString();
            }
            href = string.Format(m_url, m_pageIndex - 1);
            m_html.Append(
              string.Format("<li><a href='{0}'>&laquo;</a></li>", href));
            return m_html.ToString();
        }
        /// <summary>
        /// 生成后一页
        /// </summary>
        private string GenNext()
        {
            StringBuilder m_html = new StringBuilder();
            string href = string.Empty;
            if (m_pageIndex >= m_pageCount)
            {
                m_html.Append("<li class='disabled'><a >&raquo;</a></li>");
                return m_html.ToString();
            }
            href = string.Format(m_url, ++m_pageIndex);
            m_html.Append(
              string.Format("<li><a href='{0}'>&raquo;</a></li>", href));
            return m_html.ToString();
        }
        /// <summary>
        /// 左停靠连接
        /// </summary>
        private string GenLeftAnchor()
        {
            StringBuilder m_html = new StringBuilder();
            string href = string.Empty;
            if (m_pageIndex - m_offset > 0)
            {
                href = string.Format(m_url, 0);
                m_html.Append(string.Format("<li><a href='{0}'>1</a></li>", href));
            }

            if (m_pageIndex - m_offset > 1)
                m_html.Append(string.Format("<li><span>...</span></li>"));

            return m_html.ToString();
        }
        /// <summary>
        /// 右停靠连接
        /// </summary>
        private string GenRightAnchor()
        {
            if (m_pageIndex + m_offset >= m_pageCount - 1)
                return string.Empty;

            StringBuilder m_html = new StringBuilder();
            string href = string.Empty;

            if (m_pageIndex + m_offset < m_pageCount - 2)
                m_html.Append(string.Format("<li><span>...</span></li>"));

            if (m_pageIndex + m_offset < m_pageCount)
            {
                href = string.Format(m_url, m_pageCount - 1);
                m_html.Append(string.Format("<li><a href='{0}'>{1}</a></li>", href, m_pageCount));
            }

            return m_html.ToString();
        }
        /// <summary>
        /// 生成中间分页按钮部分
        /// </summary>
        private string GenMidArea()
        {
            StringBuilder m_html = new StringBuilder();
            string href = string.Empty;
            if (m_pageCount == 1)
            {

                m_html.Append("<li class='active'><a href='#'>1</a></li>");
                return m_html.ToString();
            }
            Action<int> addHtml = (index =>
            {
                if (index == m_pageIndex)
                {
                    href = string.Format("<li class='active'><a >{0}</a></li>", index);
                    m_html.Append(href);
                    return;
                }
                href = string.Format(m_url, index);
                href = string.Format("<li ><a href='{0}'>{1}</a></li>", href, index);
                m_html.Append(href);
            });

            int start = 0;
            int end = 0;
            if (m_pageIndex < m_offset)
            {
                start = 0;
                if (m_offset + m_offset < m_pageCount)
                    end = m_offset + m_offset;
                else
                    end = m_pageCount - 1;
            }
            else
            {
                start = m_pageIndex - m_offset;
                if (m_pageIndex + m_offset >= m_pageCount)
                    end = m_pageCount - 1;
                else
                    end = m_pageIndex + m_offset;
            }

            for (int i = 0; i < m_pageCount; i++)
            {
                if (i < start || i > end)
                    continue;
                addHtml(i + 1);
            }

            return m_html.ToString();
        }
    }
    #endregion
}