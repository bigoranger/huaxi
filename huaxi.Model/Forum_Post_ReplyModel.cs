namespace huaxi {
    public  class  Forum_Post_Reply {
        public System.Int32 ID {get; set;}
        public System.Int32 PostID {get; set;}
        public System.Int32 UserID {get; set;}
        public System.String Contents {get; set;}
        public System.DateTime CreateTime {get; set;}
        public System.DateTime? UpdateTime {get; set;}
        public System.Int32 Status {get; set;}
    }
}
