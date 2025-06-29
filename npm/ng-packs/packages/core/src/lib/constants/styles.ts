export default `
 :root .lpx-content-container .lpx-content{min-height:calc(100vh - 44px);padding:1.25em 2em 1.25em}h1,h2,h3,h4,h5,h6{color:inherit}.lpx-settings .lpx-context-menu{overflow:auto}
// a {
//     text-decoration: unset;
// }


:root select{
  cursor: pointer; /* this will show the pointer when hovering */
} 
.cdk-global-overlay-wrapper, .cdk-overlay-container {pointer-events: none;top: 0;left: 0;height: 100%;width: 100% }.cdk-overlay-container {position: fixed;z-index: 1000 }.cdk-overlay-container:empty {display: none }.cdk-global-overlay-wrapper, .cdk-overlay-connected-position-bounding-box, .cdk-overlay-pane {position: absolute;z-index: 1000;display: flex }.cdk-overlay-pane {pointer-events: auto;box-sizing: border-box;max-width: 100%;max-height: 100% }.cdk-overlay-backdrop {position: absolute;top: 0;bottom: 0;left: 0;right: 0;z-index: 1000;pointer-events: auto;transition: opacity .4s cubic-bezier(.25, .8, .25, 1);opacity: 0 }.cdk-overlay-backdrop.cdk-overlay-backdrop-showing {opacity: 1 }.cdk-high-contrast-active .cdk-overlay-backdrop.cdk-overlay-backdrop-showing {opacity: .6 }.cdk-overlay-dark-backdrop {background: rgba(0, 0, 0, .32) }.cdk-overlay-transparent-backdrop {transition: visibility 1ms linear, opacity 1ms linear;visibility: hidden;opacity: 1 }.cdk-overlay-transparent-backdrop.cdk-overlay-backdrop-showing {opacity: 0;visibility: visible }.cdk-overlay-backdrop-noop-animation {transition: none }.cdk-overlay-connected-position-bounding-box {flex-direction: column;min-width: 1px;min-height: 1px }.cdk-global-scrollblock {position: fixed;width: 100%;overflow-y: scroll }.cdk-visually-hidden {border: 0;clip: rect(0 0 0 0);height: 1px;margin: -1px;overflow: hidden;padding: 0;position: absolute;width: 1px;outline: 0;-webkit-appearance: none;-moz-appearance: none }.nz-overlay-transparent-backdrop, .nz-overlay-transparent-backdrop.cdk-overlay-backdrop-showing {opacity: 0 }.ant-select, .ant-select-dropdown {margin: 0;line-height: 1.5715;box-sizing: border-box;font-feature-settings: 'tnum';list-style: none }.ant-dropdown.ant-slide-down-appear.ant-slide-down-appear-active.ant-dropdown-placement-bottom, .ant-dropdown.ant-slide-down-appear.ant-slide-down-appear-active.ant-dropdown-placement-bottomLeft, .ant-dropdown.ant-slide-down-appear.ant-slide-down-appear-active.ant-dropdown-placement-bottomRight, .ant-dropdown.ant-slide-down-enter.ant-slide-down-enter-active.ant-dropdown-placement-bottom, .ant-dropdown.ant-slide-down-enter.ant-slide-down-enter-active.ant-dropdown-placement-bottomLeft, .ant-dropdown.ant-slide-down-enter.ant-slide-down-enter-active.ant-dropdown-placement-bottomRight, .ant-select-dropdown.ant-slide-up-appear.ant-slide-up-appear-active.ant-select-dropdown-placement-bottomLeft, .ant-select-dropdown.ant-slide-up-enter.ant-slide-up-enter-active.ant-select-dropdown-placement-bottomLeft {animation-name: antSlideUpIn }.ant-select-dropdown.ant-select-tree-dropdown {top: 100%;left: 0;position: relative;width: 100%;margin-top: 4px;margin-bottom: 4px;overflow: auto }.ant-select-dropdown-hidden, .ant-upload-list-picture-card .ant-upload-list-item-uploading .ant-upload-list-item-info .anticon-delete, .ant-upload-list-picture-card .ant-upload-list-item-uploading .ant-upload-list-item-info .anticon-eye, .ant-upload-list-picture-card .ant-upload-list-item-uploading .ant-upload-list-item-info::before {display: none }.ant-select-dropdown {top: 100%;left: 0;position: relative;width: 100%;margin-top: 4px;margin-bottom: 4px;display: block }.ant-select-dropdown .cdk-virtual-scroll-content-wrapper {right: 0 }.ant-select-dropdown .full-width {contain: initial }.ant-select-dropdown .full-width .cdk-virtual-scroll-content-wrapper {position: static }.ant-select-dropdown .full-width .cdk-virtual-scroll-spacer {position: absolute;top: 0;width: 1px }.ant-select-dropdown.ant-slide-up-appear.ant-slide-up-appear-active.ant-select-dropdown-placement-topLeft, .ant-select-dropdown.ant-slide-up-enter.ant-slide-up-enter-active.ant-select-dropdown-placement-topLeft {animation-name: antSlideDownIn }.ant-select-dropdown.ant-slide-up-leave.ant-slide-up-leave-active.ant-select-dropdown-placement-bottomLeft {animation-name: antSlideUpOut }.ant-select-dropdown.ant-slide-up-leave.ant-slide-up-leave-active.ant-select-dropdown-placement-topLeft {animation-name: antSlideDownOut }

/*** 表单验证样式 start */
.was-validated .form-control:invalid, .form-control.is-invalid{
    border-color: #c00d49 !important;
}
/*** 表单验证样式 end */

 .dignite-form-select {
  width: 100%;

  .ant-select-selector {
    background: transparent !important;
    border: none !important;
    padding: 0.475rem 1.25rem !important;
    box-shadow: none !important;
    height: auto !important;
  }

  .anticon-search,
  .anticon-down {
    display: none;
  }

  .ant-select-arrow,
  .ant-select-clear {
    right: 36px !important;
  }

  .ant-select {
    color: inherit !important;
  }

  .ant-select-selection-placeholder {
    color: inherit !important;
  }
}

.dignite-form-select.ant-select-multiple .ant-select-selection-item {
  max-width: 60% !important;
}

.dignite-form-select-dropdown {
  .ant-select-dropdown {
    background: var(--bs-light) !important;
  }

  .ant-select-item-option-active:not(.ant-select-item-option-disabled) {
    background-color: var(--bs-primary) !important;
    color: var(--bs-white) !important;
  }

  .ant-select-item-option-selected:not(.ant-select-item-option-disabled) {
    background-color: var(--lpx-brand);
    color: var(--bs-white) !important;
  }

  .ant-select-item {
    color: var(--bs-body-color);
  }
}

// /***select 选择器end */


`;
