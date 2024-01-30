export default `
/* 水平滚动条样式 */
/* 高度 */
::-webkit-scrollbar {
    width: 8px;
    height: 8px;
}

/* 背景色 */
::-webkit-scrollbar-track {
    background-color: #f5f5f5;
}

/* 滑块颜色 */
::-webkit-scrollbar-thumb {
    background-color: #c1c1c1;
    border-radius: 50px;
}

.no-round {
    border-radius: 0 !important;
}

div[overflow] {
    width: 100%;
    height: 100%;
    overflow: auto;
}

.col-2-5 {
    flex: 0 0 auto;
    width: 20%;
}

.dignite_upload_input {
    width: 120px;
    height: 120px;
    min-width: 120px;
}

.d-block {
    display: block;
}

.uploadImage {
    border: 1px solid var(--bs-gray-500);
    position: relative;
    overflow: hidden;
    font-size: 16px;

    .upload_btn_icon {
        position: absolute;
        inset: 0;
        text-align: center;
        line-height: 60px;
    }

    .upload_input {
        position: absolute;
        inset: 0;
        opacity: 0;
    }

    .uploadImage_delete {
        position: absolute;
        inset: 0;
        width: 100%;
        height: 100%;
        background-color: #00000050;
        display: flex;
        align-items: center;
        justify-content: center;
        display: none;
        transition: all 0.2s;
    }
}

.uploadImage[show]:hover .uploadImage_delete {
    display: flex;
}

@keyframes spinner-btn {
    to {
        transform: rotate(360deg);
    }
}

.spinner-btn {
    animation: spinner-btn 0.75s linear infinite;
}

`;
