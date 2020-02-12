let scene, camera, renderer, cube, container;

function init() {
    container = document.getElementById('canvas');

    scene = new THREE.Scene();

    camera = new THREE.PerspectiveCamera(
        75,
        container.offsetWidth / (window.innerHeight * 0.65),
        0.1,
        1000
    );

    renderer = new THREE.WebGLRenderer({ antialias: true, alpha: true });

    renderer.setSize(container.offsetWidth, window.innerHeight * 0.65);
    renderer.setClearColor(0x000000, 0);

    container.appendChild(renderer.domElement);

    const geometry = new THREE.BoxGeometry(3, 3, 3);
    const texture = new THREE.TextureLoader().load('../img/logo.jpg');
    const material = new THREE.MeshBasicMaterial({ map: texture });
    cube = new THREE.Mesh(geometry, material);
    scene.add(cube);

    camera.position.z = 5;
}


function animate() {
    requestAnimationFrame(animate);

    cube.rotation.x += 0.01;
    cube.rotation.y += 0.01;

    renderer.render(scene, camera);
}

function onWindowResize() {
    camera.aspect = container.offsetWidth / (window.innerHeight * 0.65);
    camera.updateProjectionMatrix();
    renderer.setSize(container.offsetWidth, window.innerHeight * 0.65);
}

init();

window.addEventListener('resize', onWindowResize, false);


animate();