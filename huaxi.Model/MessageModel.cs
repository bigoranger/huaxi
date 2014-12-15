namespace huaxi {
    public  class  Message {
        public System.Int32 ID {get; set;}
        public System.Int32 ToUserID {get; set;}
        public System.Int32 FromUserID {get; set;}
        public System.String Contents {get; set;}
        public System.DateTime CreateTime {get; set;}
        public System.Boolean IsRead {get; set;}
    }
}
