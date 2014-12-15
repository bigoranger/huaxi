namespace huaxi {
    public  class  Forum_Post {
        public System.Int32 ID {get; set;}
        public System.Int32 ForumID {get; set;}
        public System.Int32 UserID {get; set;}
        public System.String Title {get; set;}
        public System.String Contents {get; set;}
        public System.DateTime CreateTime {get; set;}
        public System.DateTime? UpdateTime {get; set;}
        public System.DateTime? LastReplyTime {get; set;}
        public System.Int32 Views {get; set;}
        public System.Int32 ReplyCount {get; set;}
        public System.Boolean IsTop {get; set;}
        public System.String Status {get; set;}
    }
}
