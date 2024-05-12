import { Renderer2, RendererFactory2, inject } from "@angular/core";
import styles from "../constants/styles";
let isAppentStyle = false
export function appentStyle() {
    /**确保该路由守卫只会执行一次 */
    if (isAppentStyle) return
    isAppentStyle = true
    const rendererFactory = inject(RendererFactory2);
    let renderer: Renderer2=rendererFactory.createRenderer(null, null)
    const style = renderer.createElement('style');
    renderer.setProperty(style, 'innerHTML', styles);
    renderer.appendChild(document.head, style);
}